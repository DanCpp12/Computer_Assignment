using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct FireProjectileSystem : ISystem
{
	public void OnUpdate(ref SystemState state)
	{
		var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
		foreach (var (Projectile,transform) in SystemAPI.Query<Projectile, LocalTransform>().WithAll<FireProjectileTag>())
		{
			var newProjectile = ecb.Instantiate(Projectile.Prefab);
			var projectileTransform = LocalTransform.FromPositionRotation(transform.Position, transform.Rotation);
			ecb.SetComponent(newProjectile, projectileTransform);
		}
		ecb.Playback(state.EntityManager);
		ecb.Dispose();
	}
}