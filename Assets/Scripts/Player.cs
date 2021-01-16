using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float health = 500f;

    [Header("Projectile")]
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] float projectileSpeed = 10;
    [SerializeField] float projectileFiringSpeed = 1;

    [SerializeField] AudioClip projectileSound;
    [SerializeField] [Range(0,1)]float projectileSoundVolume = 1;

    [Header("Player")]
    [SerializeField] AudioClip DeathSound;
    [SerializeField] [Range(0, 1)] float DeathSoundVolume = 1;

    float Xmin;
    float Xmax;
    float Ymin, Ymax;

    Coroutine firingCoroutine;

    
   


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        
    }

   



    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
       

    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, Xmin, Xmax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, Ymin, Ymax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    public void Fire()
    {
        if (Input.GetButtonDown("Fire1")) {

            firingCoroutine = StartCoroutine(FireContinuously());

        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }


    IEnumerator FireContinuously()
    {

        while (true)
        {
            GameObject laser = Instantiate(LaserPrefab, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(projectileSound, Camera.main.transform.position, projectileSoundVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringSpeed);
        }

    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        Xmin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        Xmax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        Xmin = Xmin + (float)0.5;
        Xmax = Xmax - (float)0.5;
        Ymin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        Ymax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        Ymin = Ymin + (float)0.5;
        Ymax = Ymax - (float)0.5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <= 0)
        {
            FindObjectOfType<Level>().LoadGameOverScene();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position, DeathSoundVolume);
           
        }
       
    }
    public float GetHealth()
    {
        return health;
    }
}
