using System; //for exception
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	private SocketIO.SocketIOComponent io;
	public gameController GameController;
	public GameObject PlayerPrefab;

	List<player> Players = new List<player>();

	void Start()
	{
		io = GetComponent<SocketIO.SocketIOComponent>();
		io.On("NewPlayer", (SocketIO.SocketIOEvent e) =>
		{
			JSONObject data = e.data;
			string playerID = getField(data, "playerID");
			string playerName = getField(data, "playerName");
			createNewPlayer(playerID, playerName);
		});
		io.On("ButtonDown", (SocketIO.SocketIOEvent e) =>
		{
			JSONObject data = e.data;
			string playerID = getField(data, "playerID");
			string buttonPressed = getField(data, "button");

			player foundPlayer = getPlayer(playerID);
			Vector3 moveDir = new Vector3(0f, 0f, 0f);
			Debug.Log(playerID + " pressed " + buttonPressed);
			switch (buttonPressed.ToString())
			{
				case "up":
					moveDir = new Vector3(0f, 1f, 0f);
					foundPlayer.setAimDirection("up");
					foundPlayer.move(moveDir);
					break;
				case "down":
					moveDir = new Vector3(0f, -1f, 0f);
					foundPlayer.setAimDirection("down");
					foundPlayer.move(moveDir);
					break;
				case "left":
					moveDir = new Vector3(-1f, 0f, 0f);
					foundPlayer.setAimDirection("left");
					foundPlayer.move(moveDir);
					break;
				case "right":
					moveDir = new Vector3(1f, 0f, 0f);
					foundPlayer.setAimDirection("right");
					foundPlayer.move(moveDir);
					break;
				case "fire":
					foundPlayer.FireWeapon(1);
					break;
			}
		});
	}
	player getPlayer(string id)
	{
		return Players.Find(player => player.Id == id);
	}
	void createNewPlayer(string id, string name)
	{
		try
		{
			GameObject newPlayerGO = GameObject.Instantiate(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
			newPlayerGO.transform.name = id;
			newPlayerGO.transform.parent = GameObject.Find("Players").transform;
			player newPlayer = newPlayerGO.GetComponent<player>();
			newPlayer.Id = id;
			Players.Add(newPlayer);

			Debug.Log(name = " joined the game. ID: " + id);
		}
		catch (Exception e)
		{
			Debug.LogError("failed to create new player: " + e.Message);
		}
	}
	string getField(JSONObject obj, string fieldName)
	{
		try
		{
			if (obj.HasField(fieldName))
				return obj.GetField(fieldName).ToString().Replace("\"", "");
			else
				throw (new Exception("Obj doesnt have proptery: " + fieldName));
		}
		catch (Exception e)
		{
			Debug.Log("error parsing json field: " + e.Message);
			return null;
		}
	}
}