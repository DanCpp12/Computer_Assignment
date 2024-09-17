using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct EnemyMoveSystem : ISystem
{
	//[BurstCompile]
	public void OnUpdate(ref SystemState state)
	{
		float DeltaTime = SystemAPI.Time.DeltaTime;

		new EnemyMoveJob
		{
			deltaTime = DeltaTime,
		}.Schedule();
	}
}

public partial struct EnemyMoveJob : IJobEntity
{
	public float deltaTime;

	JobHandle jobHandle;
	//[BurstCompile]
	private void Execute(ref LocalTransform transform, in Enemy enemy)
	{
		jobHandle = new();
		transform.Position += -transform.Up() * enemy.MoveSpeed * deltaTime;
		jobHandle.Complete();
	}
}