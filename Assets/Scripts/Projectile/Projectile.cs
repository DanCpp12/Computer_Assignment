using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Projectile : IComponentData
{
    public Entity Prefab;
    public float MoveSpeed;
    public float Size;
}

public struct FireProjectileTag : IComponentData, IEnableableComponent { }