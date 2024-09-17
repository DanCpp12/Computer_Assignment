using Unity.Entities;
using Unity.Transforms;
//using Unity.U2D.Entities.Physics;
using UnityEngine;

public struct Collider : IComponentData
{
    public float Size;
    public CollisionLayer BelongToLayer;
    public CollisionLayer CollideWithLayer;
}

public enum CollisionLayer
{
    Default = 1 << 0,
    Enemy = 1 << 6,
    Projectile = 1 << 7,
    Base = 1 << 8,
}