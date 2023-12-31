using UnityEngine;
namespace BattleCity
{

	public class Bullet : MonoBehaviour
	{

		public int damage = 1;
		public bool isEnemy;

		// Проверка попадания пули
		void OnTriggerEnter2D(Collider2D coll)
		{
			if (!coll.isTrigger) // Если попала в 
			{
				if (coll.transform.CompareTag("Wall"))
				{
					// Стену - просто удаляем
					Destroy(gameObject);
				}

				if (coll.transform.CompareTag("Block"))
				{
					// Кирпич - ломаем его
					Destroy(coll.transform.gameObject);
					Destroy(gameObject);
				}

				if (!isEnemy)
				{
					if (coll.transform.CompareTag("Enemy"))
					{
						// Наша пуля во вражеский танк - снижаем уровень здоровья
						EnemyTank enemy = coll.transform.GetComponent<EnemyTank>();
						enemy.HP -= damage;
						Destroy(gameObject);
					}
				}
				else
				{
					if (coll.transform.CompareTag("Player"))
					{
						// Вражеская пуля в наш танк - снижаем уровень здоровья
						PlayerTank player = coll.transform.GetComponent<PlayerTank>();
						player.HP -= damage;
						Destroy(gameObject);
					}
				}
			}
		}
	}
}