using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Health))]
[RequireComponent (typeof(VampirismAbility))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _playerRB;
    private PlayerAnimator _playerAnimator;
    private Health _health;
    private Attacker _attacker;
    private VampirismAbility _vampirismAbility;
    private IEnumerator _coroutine;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _health = GetComponent<Health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerRB = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _vampirismAbility = GetComponent<VampirismAbility>();
    }

    private void OnEnable()
    {
        _health.HealthEmptied += Die;
    }

    private void OnDisable()
    {
        _health.HealthEmptied -= Die;
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleInput()
    {
        float horizontalMove = Input.GetAxis("Horizontal");

        if (horizontalMove > 0)
        {
            _spriteRenderer.flipX = false;
            _playerAnimator.ActivateRunAnimation(Mathf.Abs(horizontalMove));
        }
        else if (horizontalMove < 0)
        {
            _spriteRenderer.flipX = true;
            _playerAnimator.ActivateRunAnimation(Mathf.Abs(horizontalMove));
        }
        else
        {
            _playerAnimator.ActivateRunAnimation(0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _vampirismAbility.ActivateAbility();
        }
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalMove * _speed, _playerRB.velocity.y);
        _playerRB.velocity = movement;
    }

    private void Jump()
    {
        if (Mathf.Approximately(_playerRB.velocity.y, 0))
        {
            _playerRB.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _playerAnimator.ActivateJumpingAnimation();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.TryGetComponent<Health>(out Health health) && enemy != null)
            {
                _attacker.ActivateAnimation += _playerAnimator.ActivateAttackAnimation;
                _coroutine = _coroutine = _attacker.Attack(enemy.GameObject());
                StartCoroutine(_coroutine);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _attacker.ActivateAnimation -= _playerAnimator.ActivateAttackAnimation;

        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
