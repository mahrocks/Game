using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{

	public int currentHealth = 3;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void TakeDamage (int amount)
	{
		currentHealth -= amount;

		if (currentHealth <= 0) {
			Destroy ();
		}
	}
}
