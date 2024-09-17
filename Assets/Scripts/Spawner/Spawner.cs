using Unity.Entities;
using Unity.Mathematics;

public struct Spawner : IComponentData
{
	public Entity Prefab;

	public float2 SpawnPosition;
	public float Offset;
	public float3 Position;

	public float NextSpawnTime;
	public float SpawnRate;

	public float WaveLength;
	public float NextWave;
	public float WaveColdown;
	public float NextWaveColdown;
}
