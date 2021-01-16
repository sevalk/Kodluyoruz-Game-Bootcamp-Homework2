using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.1f;
    [SerializeField] float maxTimeBetweenSjots = 3f;
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] float projectileSpeed = -10;

    [Header("Sound Effects")]
    [SerializeField] GameObject expPrefab;
    [SerializeField] AudioClip projectileSound;
    [SerializeField] [Range(0, 1)] float projectileSoundVolume = 1;

    [SerializeField] AudioClip DeathSound;
    [SerializeField] [Range(0, 1)] float DeathSoundVolume = 1;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenSjots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenSjots);
        }
    }

    private void Fire()
    {
        
            GameObject laser = Instantiate(LaserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(projectileSound, Camera.main.transform.position, projectileSoundVolume);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
        damageDealer.Hit();
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            GameObject exp = Instantiate(expPrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(exp, 1f);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position, DeathSoundVolume);

        }
    }
}
