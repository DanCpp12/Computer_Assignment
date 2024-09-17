using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct SpawnerSystem : ISystem
{
	public void OnCreate(ref SystemState state)
	{
		foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
		{
			spawner.ValueRW.SpawnPosition = NewSpawnPosition(spawner.ValueRO.Offset);
		}
	}

	public void OnDestroy(ref SystemState state) { }

	public void OnUpdate(ref SystemState state)
	{
		foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
		{
			if (spawner.ValueRO.NextWaveColdown < SystemAPI.Time.ElapsedTime)
			{
				if (spawner.ValueRO.NextWave > SystemAPI.Time.ElapsedTime)
				{
					if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
					{
						Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
						float3 pos = new float3(spawner.ValueRO.SpawnPosition.x + spawner.ValueRO.Position.x, spawner.ValueRO.SpawnPosition.y + spawner.ValueRO.Position.y, 0);
						state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));
						spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
						spawner.ValueRW.SpawnPosition = NewSpawnPosition(spawner.ValueRO.Offset);
					}
				}
				else { spawner.ValueRW.NextWaveColdown = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.WaveColdown; }
			}
			else { spawner.ValueRW.NextWave = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.WaveLength; }
		}
	}
	private static System.Random rand = new System.Random();
	private float2 NewSpawnPosition(float Offset)
	{
		float angle = rand.Next(360);
		float value = angle * (Mathf.PI / 180f);

		return new float2(Mathf.Cos(value) * Offset, Mathf.Sin(value));
	}
}
