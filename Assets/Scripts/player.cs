using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
	public float speed = 1.0f;

	public void move(Vector3 moveAmt)
	{
		transform.position += moveAmt * speed * Time.deltaTime;
	}
}
