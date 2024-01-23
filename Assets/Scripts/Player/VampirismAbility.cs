using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class VampirismAbility : MonoBehaviour
{
    [SerializeField] private float _radius;

    private int _damage = 10;
    private int _effectiveTime = 6;
    private Health _playerHealth;
    private float _interval = 1f;
    private bool _active = false;
    private VampirismLogo _logo;

    private void Awake()
    {
       _logo = GetComponentInChildren<VampirismLogo>();
        _playerHealth = GetComponent<Health>();
    }

    public void ActivateAbility()
    {
        StartCoroutine(StartAbility());
    }

    public IEnumerator StartAbility()
    {
        if(!_active)
        {
            _active = true;
            _logo.SetActiveTrue();
            WaitForSeconds interval = new WaitForSeconds(_interval);

            for (int i = _effectiveTime; i > 0; i--)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);

                foreach(Collider2D collider in colliders)
                {
                    if (collider.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        enemy.GetComponent<Health>().Decreace(_damage);
                        _playerHealth.Increace(_damage);
                    }
                }

                yield return interval;
            }

            _active = false;
            _logo.SetActiveFalse();
        }
        else
        {
            yield break;
        }
    }
}
