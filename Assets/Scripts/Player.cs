using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static readonly float MaxHealth = 50f;
    public static readonly float MinHealth = 0f;

    public float HealthLastChange { get; private set; }

    private UnityEvent _healthChanged = new UnityEvent();

    private float _health = MaxHealth;

    public event UnityAction HealthChanged
    {
        add => _healthChanged?.AddListener(value);
        remove => _healthChanged?.RemoveListener(value);
    }

    public void Hit(float value) => ChangeHealth(-value);

    public void Heal(float value) => ChangeHealth(value);
    
    private void ChangeHealth(float value)
    {
        _health  = Mathf.Clamp(_health + value, MinHealth, MaxHealth);

        HealthLastChange = value;

        _healthChanged.Invoke();
    }
}
