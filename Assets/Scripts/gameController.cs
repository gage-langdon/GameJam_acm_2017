using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
	public EnemySpawner Spawner;

	private bool isGameRunning = false;

	void Start()
	{
		StartGame();
	}
	void StartGame()
	{
		isGameRunning = true;
		StartCoroutine(SpawnEnemies());
	}
	IEnumerator SpawnEnemies()
	{
		while (isGameRunning)
		{
			Spawner.spawnEnemy();
			yield return new WaitForSeconds(1);
		}
	}
}
