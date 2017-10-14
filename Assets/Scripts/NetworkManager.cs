using System; //for exception
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	private SocketIO.SocketIOComponent io;
	public GameObject playerPrefab;
	public gameController GameController;

	public class player
	{
		public string id { get; set; }
		public Transform transform { get; set; }
		public Vector3 newPos { get; set; }
		public Quaternion newRot { get; set; }
		//public networkPlayerController network;

	}
	List<player> otherPlayers = new List<player>();

	void Start()
	{
		io = GetComponent<SocketIO.SocketIOComponent>();

		io.On("joined-game", (SocketIO.SocketIOEvent e) =>
		{
			JSONObject data = e.data;
			Debug.Log("Joined game " + data);
		});
		io.On("ButtonDown", (SocketIO.SocketIOEvent e) =>
		{
			JSONObject data = e.data;
			string buttonPressed = getField(data, "button");
			GameController.MovePlayer(buttonPressed);
		});
	}

	player getPlayer(string id)
	{
		return otherPlayers.Find(player => player.id == id);
	}

	string getField(JSONObject obj, string fieldName)
	{
		return obj.GetField(fieldName).ToString().Replace("\"", "");
	}
}