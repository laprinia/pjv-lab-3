﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth=100;
    public int currentHealth;
    public Healthbar Healthbar;

    private void Start()
    {
        currentHealth = maxHealth;
        Healthbar.SetMaximumHealth(100);
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Healthbar.SetHealth(currentHealth);
    }
}
