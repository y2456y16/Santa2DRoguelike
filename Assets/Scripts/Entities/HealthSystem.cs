using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f; //무적시간

    private CharacterStatsHandler _statsHandler;
    private float _timeSinceLastChange = float.MaxValue;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;
    public bool CanResurrection = false;
    public float CurrentHealth { get; private set; }

    public float MaxHealth => _statsHandler.CurrentStats.maxHealth;

    private void Awake()
    {
        _statsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statsHandler.CurrentStats.maxHealth;
    }

    private void Update()
    {
        if (_timeSinceLastChange < healthChangeDelay)
        {
            _timeSinceLastChange += Time.deltaTime;
            if (_timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || _timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        _timeSinceLastChange = 0f;
        CurrentHealth += change;
        //CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth; -> player hp가 full인 상태에서 블루 하트 먹을 시 정상적으로 반영이 되지 않아서 생략.
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            UIManager.Instance.BrokenHeart();
            OnDamage?.Invoke();
        }

        if (CurrentHealth <= 0f)
        {
            if (CanResurrection)
            {
                Resurrection();
            }
            else
            {
                CallDeath();
            }
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }

    private void Resurrection()
    {
        CurrentHealth = MaxHealth;
        UIManager.Instance.MakeHeart(CanResurrection);
        CanResurrection = false;
    }
}