using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);

    public float CurrentHealth { get; }
    public float MaxHealth { get; set; }
}
