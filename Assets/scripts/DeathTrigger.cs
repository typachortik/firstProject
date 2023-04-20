using System.Collections;
using UnityEngine;


namespace Assets.scripts
{
    public class DeathTrigger : MonoBehaviour
    {
        [SerializeField] private float _height = 1f;
        [SerializeField] private float _speed = 10f;


        private float _startHeight;
        private Rigidbody2D _rigidbody;

        

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _startHeight = transform.position.y;
        }

        private void FixedUpdate()
        {
            if (transform.position.y >= _height)
            {
                Move(Vector3.down);
            }
            if (transform.position.y <= _height)
            {
                Move(Vector3.up);
            }
        }

        private void Move(Vector3 movementVector)
        {
            _rigidbody.velocity = movementVector * _speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null)
            {
                if(other.TryGetComponent<PlayerMovement>(out var player))
                {
                    player.HandlePlayerDeath();
                }
            }
        }
    }
}