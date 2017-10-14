using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
	public GameObject PinataPrefab;
	Transform[] Spawners;
	List<Enemy> Enemies = new List<Enemy>();

	void Start()
	{
		Spawners = GetComponentsInChildren<Transform>();
		StartCoroutine(moveEnemies());
	}
	public void spawnEnemy()
	{
		int rand = Random.Range(0, Spawners.Length);
		Vector3 spawnPos = new Vector3(Spawners[rand].position.x, Spawners[rand].position.y, Spawners[rand].position.z);
		GameObject newEnemy = GameObject.Instantiate(PinataPrefab, spawnPos, PinataPrefab.transform.rotation);
		newEnemy.transform.parent = GameObject.Find("Enemies").transform;

		float loc = getRelativePos(newEnemy.transform.position);
		if (loc < 1)
			newEnemy.transform.Rotate(0f, 0f, 180f);


		Enemies.Add(newEnemy.GetComponent<Enemy>());
	}
	IEnumerator moveEnemies()
	{
		while (true)
		{
			if (Enemies.Count > 0)
			{
				foreach (Enemy e in Enemies)
				{
					if (e.transform)
					{
						float enemyPos = getRelativePos(e.transform.position);
						float moveAmount = enemyPos *= e.Speed;
						e.move(moveAmount);
					}
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
	int getRelativePos(Vector3 pos)
	{
		int middleOfScreen = Screen.width / 2;
		Vector3 eWorldPos = Camera.main.WorldToScreenPoint(pos);
		float enemyPos = eWorldPos.x;
		if (enemyPos < middleOfScreen) return 1;
		else if (enemyPos > middleOfScreen) return -1;
		else return 0;
	}

}
