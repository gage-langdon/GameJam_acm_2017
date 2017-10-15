using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private float speed = 0.01f;
	private int health = 3;
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
	void OnTriggerEnter(Collider col)
	{
		Debug.Log("collided with " + col.transform.name);

		Bullet bullet = col.GetComponent<Bullet>();
		if (bullet)
		{
			health--;
			GameObject.Destroy(bullet.gameObject);
			if (health <= 0)
			{
				GameObject.Destroy(this.transform.gameObject);
			}
		}
		else
		{
			fence Fence = col.GetComponent<fence>();
			if (Fence)
			{
				GameObject.Destroy(this.transform.gameObject);
			}
		}

	}
}
