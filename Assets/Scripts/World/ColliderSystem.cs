using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Physics;
using System.Buffers;
using static UnityEngine.EventSystems.EventTrigger;
using System;

[UpdateAfter(typeof(BeginFixedStepSimulationEntityCommandBufferSystem))]
public partial struct ColliderSystem : ISystem
{
	DamageAndHealth ownerDamageAndHealt;
	DamageAndHealth hitDamageAndHealth;
	
	private void OnUpdate(ref SystemState state)
	{
		EntityManager entityManager = state.EntityManager;

		NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);
		foreach (Entity entity in entities)
		{
			if (entityManager.HasComponent<Collider>(entity))
			{
				RefRO<LocalToWorld> colliderTransform = SystemAPI.GetComponentRO<LocalToWorld>(entity);
				RefRO<Collider> collider = SystemAPI.GetComponentRO<Collider>(entity);
				ownerDamageAndHealt = entityManager.GetComponentData<DamageAndHealth>(entity);

				PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
				NativeList<ColliderCastHit> hits = new NativeList<ColliderCastHit>(Allocator.Temp);
				physicsWorld.SphereCastAll(colliderTransform.ValueRO.Position, collider.ValueRO.Size / 2,
					float3.zero, 1, ref hits, new CollisionFilter { BelongsTo = (uint)collider.ValueRO.BelongToLayer, CollidesWith = (uint)collider.ValueRO.CollideWithLayer });

				foreach (ColliderCastHit hit in hits)
				{
                    if (entityManager.HasComponent<DamageAndHealth>(hit.Entity))
					{
						hitDamageAndHealth = entityManager.GetComponentData<DamageAndHealth>(hit.Entity);

						hitDamageAndHealth.Health -= ownerDamageAndHealt.Damage;
						ownerDamageAndHealt.Health -= hitDamageAndHealth.Damage;

						entityManager.SetComponentData(hit.Entity, hitDamageAndHealth);
						entityManager.SetComponentData(entity, ownerDamageAndHealt);

						if (hitDamageAndHealth.Health <= 0)
						{
							if (entityManager.HasComponent<PlayerTag>(hit.Entity))
							{
								float3 pos = new float3(0,-99999999999, 0);
								entityManager.SetComponentData(hit.Entity, LocalTransform.FromPosition(pos));
							}
							else { entityManager.DestroyEntity(hit.Entity); }
						}
						if (ownerDamageAndHealt.Health <= 0)
						{
							if (entityManager.HasComponent<PlayerTag>(entity))
							{
								float3 pos = new float3(0, -99999999999, 0);
								entityManager.SetComponentData(entity, LocalTransform.FromPosition(pos));
							}
							else { entityManager.DestroyEntity(entity); }
						}
					}
				}
				hits.Dispose();
			}
		}
		entities.Dispose();
	}
}
