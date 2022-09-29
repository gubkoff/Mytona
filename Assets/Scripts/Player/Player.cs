using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private float Damage = 1;
    [SerializeField] private float MoveSpeed = 3.5f;
    [SerializeField] private float Health = 3;
    [SerializeField] private float MaxHealth = 3;

    public Action<int> OnWeaponChange = null;
    public Action<float, float> OnHPChange = null;
    public Action OnUpgrade = null;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public float GetDamage() {
        return Damage;
    } 
    
    public float GetMoveSpeed() {
        return MoveSpeed;
    }
    
    public float GetMaxHealth() {
        return MaxHealth;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    
    public void TakeDamage(float amount)
    {
        if (Health <= 0)
            return;
        Health -= amount;
        if (Health <= 0)
        {
            EventBus.Pub(EventBus.PLAYER_DEATH);
        }
        OnHPChange?.Invoke(Health, -amount);
    }
    
    public void Heal(float amount)
    {
        if (Health <= 0)
            return;
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        OnHPChange?.Invoke(Health, amount);
    }


    public void Upgrade(float hp, float dmg, float ms)
    {
        Damage += dmg;
        Health += hp;
        MaxHealth += hp;
        MoveSpeed += ms;
        OnUpgrade?.Invoke();
        OnHPChange?.Invoke(Health, hp);
    }

    public void ChangeWeapon(int type)
    {
        OnWeaponChange?.Invoke(type);
    }
}