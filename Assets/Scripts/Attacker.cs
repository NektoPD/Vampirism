using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Attacker : MonoBehaviour
{
    private float _nextAttackTime;
    public event UnityAction ActivateAnimation;

    public int Damage { get; private set; } = 10;

    public IEnumerator Attack(GameObject target)
    {
        while (true)
        {
            if (Time.time > _nextAttackTime)
            {
                if (target.TryGetComponent<Health>(out Health health))
                {
                    ActivateAnimation?.Invoke();
                    health.Decreace(Damage);
                }

                float attackRate = 1;

                _nextAttackTime = Time.time + attackRate;
            }

            yield return null;
        }
    }
}
