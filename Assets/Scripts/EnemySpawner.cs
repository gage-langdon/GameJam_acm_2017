using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
	public GameObject PinataPrefab;
	Transform[] Spawners;
	List<Enemy> Enemies = new List<Enemy>();

	// Use this for initialization
	void Start()
	{
		Spawners = GetComponentsInChildren<Transform>();
		StartCoroutine(moveEnemies());
	}
	public void spawnEnemy()
	{
		int rand = Random.Range(0, Spawners.Length);
		Vector3 spawnPos = new Vector3(Spawners[rand].position.x, Spawners[rand].position.y, Spawners[rand].position.z);
		GameObject newEnemy = GameObject.Instantiate(PinataPrefab, spawnPos, Quaternion.identity);
		newEnemy.transform.parent = GameObject.Find("Enemies").transform;


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
						int middleOfScreen = Screen.width / 2;
						Vector3 eWorldPos = Camera.current.WorldToScreenPoint(e.transform.position);
						float enemyPos = eWorldPos.x;
						float moveAmount = 1f;
						if (enemyPos > middleOfScreen)
							moveAmount = -1f;
						else if (enemyPos == middleOfScreen)
							e.HasHitMiddle = true;

						moveAmount *= e.Speed;
						if (!e.HasHitMiddle)
							e.move(moveAmount);
					}
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

}
