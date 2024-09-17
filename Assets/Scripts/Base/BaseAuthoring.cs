using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class BaseAuthoring : MonoBehaviour
{
	public float Health;
	public float SpawnPositionX;
	public float SpawnPositionY;
	public float RespawnDelay;

	//public GameObject Player;

	class BaseBaker : Baker<BaseAuthoring>
	{
		public override void Bake(BaseAuthoring authoring)
		{
			Entity BaseEntity = GetEntity(TransformUsageFlags.Dynamic);

			AddComponent(BaseEntity, new Base
			{
				SpawnPosition = new float2(authoring.SpawnPositionX, authoring.SpawnPositionY),
				ResspawnTime = authoring.RespawnDelay,
				NextSpawnTime = 0,
			});
			AddComponent(BaseEntity, new DamageAndHealth
			{
				Damage = 100,
				Health = authoring.Health,
			});
		}
	}
}
