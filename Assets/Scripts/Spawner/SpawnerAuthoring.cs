using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SpawnerAuthoring : MonoBehaviour
{
	public GameObject Prefab;
	public float Radius;
	public float SpawnRate;
	public float WaveLength;
	public float WaveColdown;

	private float2 NewSpawnPosition(float Offset)
	{
		System.Random rand = new System.Random();
		float angle = rand.Next(360);
		float value = angle * (Mathf.PI / 180f);

		return new float2(Mathf.Cos(value) * Offset, Mathf.Sin(value));
	}

	class SpawnerBaker : Baker<SpawnerAuthoring>
	{
		public override void Bake(SpawnerAuthoring authoring)
		{
			Entity entity = GetEntity(TransformUsageFlags.Dynamic);

			AddComponent(entity, new Spawner
			{
				Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
				SpawnPosition = new float2(authoring.NewSpawnPosition(authoring.Radius).x + authoring.transform.position.x, authoring.NewSpawnPosition(authoring.Radius).y + authoring.transform.position.y),
				Offset = authoring.Radius,
				Position = authoring.transform.position,
				NextSpawnTime = 0,
				SpawnRate = authoring.SpawnRate,
				WaveLength = authoring.WaveLength,
				WaveColdown = authoring.WaveColdown,
				NextWave = 0,
				NextWaveColdown = 0,
			});
		}
	}
}
