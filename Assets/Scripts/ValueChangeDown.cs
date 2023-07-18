using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChangeDown : MonoBehaviour
{
    [SerializeField] private Slider _healthbar;
    [SerializeField] private float _animationSpeed;
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
        float speedBySecond = _value / _animationSpeed;
        float speedByUpdate = speedBySecond * Time.deltaTime;

        float frameCount = _value / speedByUpdate;

        for (int frame = 0; frame < frameCount; frame++)
        {
            _healthbar.value = Mathf.Clamp(_healthbar.value + speedByUpdate, _healthbar.minValue, _healthbar.maxValue);
            yield return new WaitForFixedUpdate();
        }

        //while (Mathf.Abs(_value) > _epsilon)
        //{
        //    float speedBySecond = _value / _animationSpeed;
        //    float speedByUpdate = Mathf.Round(speedBySecond * Time.deltaTime * 1000)/1000;

        //    //_value = Mathf.Clamp(_value - speedByUpdate, 0, 0);

        //    if (speedByUpdate > 0)
        //    {
        //        _value = Mathf.Clamp(_value - speedByUpdate, 0, _value);
        //    }
        //    else
        //    {
        //        _value = Mathf.Clamp(_value - speedByUpdate, _value, 0);
        //    }

        //    _healthbar.value = Mathf.Clamp(_healthbar.value + speedByUpdate, _healthbar.minValue, _healthbar.maxValue);

        //    yield return new WaitForFixedUpdate();
        //}
    }
}
