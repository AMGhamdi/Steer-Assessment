using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="Unit",menuName ="Scriptable Objects / Unit")]
public class Unit : ScriptableObject
{
    public float HP;
    public float attackDamage;
    public float attackSpeed;
    public float movementSpeed;
    public float attackRange;
}
