using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerSystem : ISystem
{
	private EntityManager entityManager;
	private Entity InputEntity;
	private Input input;

	//[BurstCompile]
	public void OnUpdate(ref SystemState state)
	{
		entityManager = state.EntityManager;
		InputEntity = SystemAPI.GetSingletonEntity<Input>();
		input = entityManager.GetComponentData<Input>(InputEntity);

		float DeltaTime = SystemAPI.Time.DeltaTime;
		float2 MousePosition = MouseInWorld(ref state);

		new PlayerMoveJob
		{
			deltaTime = DeltaTime,
			input = input,
			MouseWorldPosition = MousePosition,
		}.Schedule();

		
	}
	private float2 MouseInWorld(ref SystemState state)
	{
		return (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(input.MousePos.x, input.MousePos.y, 0));
	}

	public void OnStopRunning(ref SystemState state)
	{
		InputEntity = Entity.Null;
	}
}

public partial struct PlayerMoveJob : IJobEntity
{
	public float deltaTime;
	public Input input;
	public float2 MouseWorldPosition;

	//[BurstCompile]
	private void Execute(ref LocalTransform transform, in Player player)
	{
		transform.Position.xy += input.Movement * player.MoveSpeed * deltaTime;

		Vector2 dir = (Vector2)MouseWorldPosition - (Vector2)transform.Position.xy;
		float angle = math.degrees(math.atan2(dir.y, dir.x));
		transform.Rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
	}
}

