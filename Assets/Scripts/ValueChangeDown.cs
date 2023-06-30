using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChangeDown : MonoBehaviour
{
    [SerializeField] private Slider _healthbar;
    [SerializeField] private float _loopTime;
    [SerializeField] private Player _player;

    private float _value = 0;
    private float _epsilon = 0.01f;
    private Coroutine _coroutine;

    private void Start()
    {
        _healthbar.maxValue = Player.MaxHealth;
        _healthbar.minValue = Player.MinHealth;
        _healthbar.value = Player.MaxHealth;

        _player.HealthChanged += StartMovement;
    }

    public void StartMovement()
    {
        _value = _player.HealthLastChange;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(MoveSlider());      
    }
    
    private IEnumerator MoveSlider()
    {         
        while (Mathf.Abs(_value) > _epsilon)
        {
            float speedBySecond = _value / _loopTime;
            float speedByUpdate = Mathf.Round(speedBySecond * Time.deltaTime * 1000)/1000;

            if (speedByUpdate > 0)
            {
                _value = Mathf.Clamp(_value - speedByUpdate, 0, _value);
            }
            else
            {
                _value = Mathf.Clamp(_value - speedByUpdate, _value, 0);
            }

            _healthbar.value = Mathf.Clamp(_healthbar.value + speedByUpdate, _healthbar.minValue, _healthbar.maxValue);

            yield return new WaitForFixedUpdate();
        }
    }
}
