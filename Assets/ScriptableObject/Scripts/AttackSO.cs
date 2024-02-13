using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "TopDownController/Attacks/Player", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float delay;
    public float power;
    public LayerMask target;

    [Header("Knock Back Info")]
    public bool isOnKnockback;
    public float knockbackPower;
    public float knockbackTime;
}