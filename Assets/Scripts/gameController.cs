using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
	public string controlUp = "w";
	public string controlDown = "s";
	public string controlLeft = "a";
	public string controlRight = "d";
	public player CurrentPlayer;

	void Update()
	{
		HandleUserInput();
	}
	void HandleUserInput()
	{
		var moveAmt = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		CurrentPlayer.move(moveAmt);
	}
	public void MovePlayer(string direction)
	{
		Debug.Log("move player: " + direction);

		if (direction == "up") Debug.Log("test test");

		Vector3 moveDir = new Vector3(0f, 0f, 0f);
		switch (direction.ToString())
		{
			case "up":
				Debug.Log("hit up");
				moveDir = new Vector3(0f, 1f, 0f);
				break;
			case "down":
				Debug.Log("hit down");
				moveDir = new Vector3(0f, -1f, 0f);
				break;
			case "left":
				Debug.Log("hit left");
				moveDir = new Vector3(-1f, 0f, 0f);
				break;
			case "right":
				Debug.Log("hit right");
				moveDir = new Vector3(1f, 0f, 0f);
				break;
		}
		Debug.Log(moveDir);
		CurrentPlayer.move(moveDir);
	}
}
