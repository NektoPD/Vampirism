using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private const string DeathComponentName = "Death";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ActivateDeathAnimation()
    {
        _animator.SetTrigger(DeathComponentName);
    }
}
