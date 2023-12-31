using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BattleCity
{

	public class GameControl : MonoBehaviour
	{

		public static bool playerDead;
		public static int score;
		public Transform[] enemySpawn;
		public float enemySpawnTime = 30;
		public int maxEnemy = 5;
		public Transform playerSpawn;
		public GameObject player;
		public GameObject enemy;
		public Text scoreText;
		public Text tankText;

		void Start()
		{
			maxEnemy = maxEnemy * 3;
			playerDead = false;
			score = 0;
			Instantiate(player, playerSpawn.position, Quaternion.identity);
			StartCoroutine(WaitEnemySpawn(enemySpawnTime));
		}

		// Таймер спауна врагов
		IEnumerator WaitEnemySpawn(float t)
		{
			foreach (Transform obj in enemySpawn)
			{
				maxEnemy--;
				Instantiate(enemy, obj.position, Quaternion.identity);
			}
			yield return new WaitForSeconds(t);
			if (maxEnemy > 0) StartCoroutine(WaitEnemySpawn(enemySpawnTime));
		}

		// Обновление счета и кол-ва врагов
		void OnGUI()
		{
			scoreText.text = "Счет: " + score.ToString();
			tankText.text = "Осталось врагов: " + maxEnemy;
		}

		void Update()
		{
			if (playerDead)
			{
				playerDead = false;
			}
		}
	}
}