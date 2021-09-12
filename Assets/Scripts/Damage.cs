using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damage = 20;

    public int GetDamage()
    {
        return damage;
    }
}



