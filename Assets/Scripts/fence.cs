using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fence : MonoBehaviour {
	
	public GameObject model;
	public int health;

	// Use this for initialization
	void Start () {
		health = 3; 
	}
	
	// Update is called once per frame
	void Update () {
		if (health > 0) {
			model.SetActive (true); 
			health = 3; 
		}

		if (health <= 0) {
			model.SetActive (false); 
		}
	}
}
