using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ColliderAuthoring : MonoBehaviour
{
    public float Size = 1;
    public CollisionLayer BelongToLayer;
    public CollisionLayer CollideWithLayer;

    public class ColliderBaker : Baker<ColliderAuthoring>
    {
        public override void Bake(ColliderAuthoring authoring)
        {
            Entity colliderAuthering = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(colliderAuthering, new Collider 
            {
                Size = authoring.Size,
                BelongToLayer = authoring.BelongToLayer,
                CollideWithLayer = authoring.CollideWithLayer,
            });
        }
    }
}
