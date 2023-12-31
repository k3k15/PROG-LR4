namespace BattleCity
{

    using UnityEngine;

    public class PlayerTank : MonoBehaviour
    {

        const float bulletSpeed = 3f;
        public Transform explosion;
        public Animator _animator;
        public Transform tank;
        public Transform gun;
        public Rigidbody2D bullet;
        public int HP = 2;
        public float speed = 100f;
        public Vector2 moveDirection;
        public Vector3 rotation;
        public CheckMove check;
        public Rigidbody2D body;

        void Start()
        {
            _animator.speed = 0;
            body = GetComponent<Rigidbody2D>();
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
            CheckHealth();
            MoveAndShoot();
        }

        void CheckHealth()
        {
            if (HP <= 0)
            {
                GameControl.playerDead = true;
                float randomZ = Random.Range(0, 360f);
                Destroy(gameObject);
            }
        }

        public void MoveAndShoot()
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection = new Vector2(0, 1);
                rotation = new Vector3(0, 0, 0);
                _animator.speed = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveDirection = new Vector2(0, -1);
                rotation = new Vector3(0, 0, 180);
                _animator.speed = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                moveDirection = new Vector2(-1, 0);
                rotation = new Vector3(0, 0, 90);
                _animator.speed = 1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = new Vector2(1, 0);
                rotation = new Vector3(0, 0, -90);
                _animator.speed = 1;
            }
            else
            {
                moveDirection = new Vector2(0, 0);
                _animator.speed = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody2D bulletInstance = Instantiate(bullet, gun.position, Quaternion.identity) as Rigidbody2D;
                bulletInstance.velocity = gun.TransformDirection(Vector2.up * bulletSpeed);
            }

            tank.localRotation = Quaternion.Euler(rotation);
        }
    }
}