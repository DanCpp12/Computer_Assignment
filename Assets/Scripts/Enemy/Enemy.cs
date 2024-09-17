using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Enemy : IComponentData
{
    public float MoveSpeed;
    public float Damage;
    public float Health;
}
public struct EnemyTag : IComponentData { }