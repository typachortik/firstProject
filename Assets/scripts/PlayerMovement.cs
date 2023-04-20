using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _speed;
    [SerializeField] private TriggerScript _trigger;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _heightThreshold;
    [SerializeField] private TextMeshProUGUI _deathCounter;
    [SerializeField] private TextMeshProUGUI _pointsCounter;
    [SerializeField] private GameObject _loseWindow;

    public event Action Triggered = delegate { };


    bool inAir;

    private int _lifeCount = 5;
    private int _points = 0;


    private void Update()
    {
        if (inAir)
        {
            if (transform.position.y < _heightThreshold)
            {
                HandlePlayerDeath();
            }
        }
    }

    public void HandlePlayerDeath()
    {
        _lifeCount--;
        if (_lifeCount > 0)
        {
            _deathCounter.text = $" {_lifeCount}";
            ResetPosition();
        }
        else
        {
            ShowLoseWindow();
            _lifeCount = 0;
        }
        
    }

    private void ShowLoseWindow()
    {
        _loseWindow.SetActive(true);
    }

    public void ResetGame()
    {
        _deathCounter.text = $" {_lifeCount}";
        _loseWindow.SetActive(false);
        _lifeCount = 5;
        ResetPosition();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(typeof(PlayerMovement), out var playerComponent))
        {
            Triggered?.Invoke();
            obstacle.SetActive(false);
            _points++;
            _pointsCounter.text = $" {_points}";
        }
    }

    private void ResetPosition()
    {
        transform.position = _spawnPoint.position;
    }

    private void FixedUpdate()
    {
        var verticalAxis = Input.GetAxis("Vertical");
        var horizontalAxis = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalAxis, verticalAxis) * _speed;
        if (Input.GetKey(KeyCode.Space) && !inAir)
        {
            _rigidbody2D.AddForce(new Vector2(0,80), ForceMode2D.Impulse);
            Debug.Log(inAir);
        }
        //inAir = _rigidbody2D.velocity.y != 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        inAir = !(collision.gameObject.layer == LayerMask.NameToLayer("ground"));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        inAir = true;
    }

}
