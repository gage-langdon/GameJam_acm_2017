using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fence : MonoBehaviour
{

	public GameObject model;
	public int health;

	// Use this for initialization
	void Start()
	{
		health = 1;
	}
	void OnTriggerEnter(Collider col)
	{
		Debug.Log("collided with " + col.transform.name);

		Enemy enemy = col.GetComponent<Enemy>();
		if (enemy)
		{
			health--;
			if (health <= 0)
			{
				GameObject.Destroy(this.transform.gameObject);
			}
		}
	}
}
