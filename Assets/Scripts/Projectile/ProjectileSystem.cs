using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct ProjectileSystem : ISystem
{
	private EntityManager entityManager;
	public void OnUpdate(ref SystemState state)
	{
		float DeltaTime = SystemAPI.Time.DeltaTime;

		new ProjectileMoveJob
		{
			deltaTime = DeltaTime,
		}.Schedule();
	}
}

public partial struct ProjectileMoveJob : IJobEntity
{
	public float deltaTime;
	//[BurstCompile]
	private void Execute(ref LocalTransform transform, in Projectile projectile)
	{
		transform.Position += transform.Up() * projectile.MoveSpeed * deltaTime;
	}
}