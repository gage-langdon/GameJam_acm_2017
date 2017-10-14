using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
	public float speed = 1.0f;
	private string id;
	private string playerName;

	// getters and setters
	public string Id { get; set; }
	public string PlayerName { get; set; }

	// actions
	public void move(Vector3 moveAmt)
	{
		transform.position += moveAmt * speed * Time.deltaTime;
	}
}
