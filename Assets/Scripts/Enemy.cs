using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private float speed = 0.2f;
	private bool hasHitMiddle = false;

	public float Speed
	{
		get { return speed; }
		set { speed = value; }
	}
	public bool HasHitMiddle
	{
		get { return hasHitMiddle; }
		set { hasHitMiddle = value; }
	}

	public void move(float amount)
	{
		transform.position = new Vector3(
			transform.position.x + amount,
			transform.position.y,
			transform.position.z);
	}
}
