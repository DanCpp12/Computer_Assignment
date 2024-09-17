using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
	public float MoveSpeed;
	public float Damage;
	public float Health;

	class EnemyBaker : Baker<EnemyAuthoring>
	{
		public override void Bake(EnemyAuthoring authoring)
		{
			Entity EnemyEntity = GetEntity(TransformUsageFlags.Dynamic);

			AddComponent(EnemyEntity, new Enemy
				{
					MoveSpeed = authoring.MoveSpeed,
				});
			AddComponent<EnemyTag>(EnemyEntity);

			AddComponent(EnemyEntity, new DamageAndHealth
			{
				Health = authoring.Health,
				Damage = authoring.Damage,
			});
		}
	}
}
