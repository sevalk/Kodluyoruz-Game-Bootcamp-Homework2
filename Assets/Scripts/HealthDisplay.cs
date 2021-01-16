using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
	Text scoreText;
	Player player;

	// Use this for initialization
	void Start()
	{
		scoreText = GetComponent<Text>();
		player = FindObjectOfType<Player>();
	}

	// Update is called once per frame
	void Update()
	{
		scoreText.text = player.GetHealth().ToString();
	}
}
