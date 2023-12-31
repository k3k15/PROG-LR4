using UnityEngine;
using System.Collections;

namespace BattleCity
{

	public class EnemyTank : MonoBehaviour
	{

		public int score = 50;
		public int HP = 1;
		public float speed = 100f;
		public Animator _animator;
		public Transform tank;
		public Transform gun;
		public Rigidbody2D bullet;
		public float bulletSpeed = 3f;
		public float minFireTime = 1.5f;
		public float maxFireTime = 3.5f;
		public float minMoveTime = 0.5f;
		public float maxMoveTime = 5.5f;
		public CheckMove check;
		private Rigidbody2D body;
		private Vector2 moveDirection;
		private Vector3 rotation;
		private int move;
		private bool fire;

		void Start()
		{
			fire = false;
			move = 0;
			_animator.speed = 0;
			body = GetComponent<Rigidbody2D>();
			StartCoroutine(WaitMove(Random.Range(minMoveTime, maxMoveTime)));
			StartCoroutine(WaitFire(Random.Range(minFireTime, maxFireTime)));
		}

		IEnumerator WaitFire(float t)
		{
			yield return new WaitForSeconds(t);
			fire = true;
			StartCoroutine(WaitFire(Random.Range(minFireTime, maxFireTime)));
		}

		IEnumerator WaitMove(float t)
		{
			move = Random.Range(0, 4);
			yield return new WaitForSeconds(t);
			StartCoroutine(WaitMove(Random.Range(minMoveTime, maxMoveTime)));
		}

		void FixedUpdate()
		{
			if (!check.target) body.AddForce(moveDirection * speed);

			if (Mathf.Abs(body.velocity.x) > speed / 100f)
			{
				body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed / 100f, body.velocity.y);
			}
			if (Mathf.Abs(body.velocity.y) > speed / 100f)
			{
				body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed / 100f);
			}
		}

		void Update()
		{
			CheckHP();
			MoveAndShoot();
		}

		void CheckHP()
		{
			if (HP <= 0)
			{
				GameControl.score += score;
				float randomZ = Random.Range(0, 360f);
				Destroy(gameObject);
			}
		}

		void MoveAndShoot()
		{
			switch (move)
			{
				case 1:
					moveDirection = new Vector2(0, 1);
					rotation = new Vector3(0, 0, 0);
					_animator.speed = 1;
					break;

				case 2:
					moveDirection = new Vector2(0, -1);
					rotation = new Vector3(0, 0, 180);
					_animator.speed = 1;
					break;

				case 3:
					moveDirection = new Vector2(-1, 0);
					rotation = new Vector3(0, 0, 90);
					_animator.speed = 1;
					break;

				case 4:
					moveDirection = new Vector2(1, 0);
					rotation = new Vector3(0, 0, -90);
					_animator.speed = 1;
					break;

				default:
					moveDirection = new Vector2(0, 0);
					_animator.speed = 0;
					break;
			}

			if (fire)
			{
				fire = false;
				Rigidbody2D bulletInstance = Instantiate(bullet, gun.position, Quaternion.identity) as Rigidbody2D;
				bulletInstance.velocity = gun.TransformDirection(Vector2.up * bulletSpeed);
			}

			tank.localRotation = Quaternion.Euler(rotation);
		}
	}
}