using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    TakeDamageResult TakeDamage(float damageAmount);
}

public enum TakeDamageResult
{
    Normal, 
    Destroy,
    Blocked 
}