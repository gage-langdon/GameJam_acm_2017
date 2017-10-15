using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinata : MonoBehaviour {

	int health; 
	public float speed; 

	// Use this for initialization
	void Start () {
		health = 3; 
		speed = 1; 
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.right * Time.deltaTime * speed; 
	}

	void OnCollisionEnter(){
			speed = 0;
	}

	void OnHit(){
		health = health--; 
		if (health <= 0) {
			Destroy (gameObject); 
		}
	}
}
