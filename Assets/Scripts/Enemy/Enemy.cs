using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private float _speed = 4;
    private Health _health;
    private Attacker _attacker;
    private bool IsAttacking = false;
    private IEnumerator _coroutine;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _health = GetComponent<Health>();
        _health.HealthEmptied += Die;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out Player player))
        {
            IsAttacking = true;
            _coroutine = _attacker.Attack(player.GameObject());
            StartCoroutine(_coroutine);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsAttacking = false;

        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void OnDisable()
    {
        _health.HealthEmptied -= Die;
    }

    public void FollowTarget(Vector2 targetPosition)
    {
        if (!IsAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}