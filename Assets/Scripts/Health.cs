using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float _maxAmount = 150;
    private float _minAmount = 0;
    private float _currentAmount;

    public event UnityAction HealthEmptied;
    public event UnityAction HealthChanged;

    public float CurrentAmount => _currentAmount;
    public float MaxAmount => _maxAmount;

    private void Awake()
    {
        _currentAmount = _maxAmount;
    }

    public void Increace(int amount)
    {
        if (_currentAmount + amount < _maxAmount)
        {
            _currentAmount += amount;
            HealthChanged?.Invoke();
        }
        else
        {
            _currentAmount = _maxAmount;
            HealthChanged?.Invoke();
        }
    }

    public void Decreace(int amount)
    {
        if (_currentAmount > _minAmount)
        {
            _currentAmount -= amount;
            HealthChanged?.Invoke();
        }
        else if (_currentAmount <= _minAmount)
        {
            HealthEmptied?.Invoke();
        }
    }
}
