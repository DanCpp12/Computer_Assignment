using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ProjectileAuthothoring : MonoBehaviour
{
	public float ProjectileSpeed;
	public float ProjectileDamage;
	public float ProjectileHealth;

	public class ProjectileBaker : Baker<ProjectileAuthothoring>
	{
		public override void Bake(ProjectileAuthothoring authoring)
		{
			Entity entity = GetEntity(TransformUsageFlags.Dynamic);
			AddComponent(entity, new Projectile
			{
				MoveSpeed = authoring.ProjectileSpeed,
				Size = 1,
			});

			AddComponent(entity, new DamageAndHealth
			{
				Health = authoring.ProjectileHealth,
				Damage = authoring.ProjectileDamage,
			});
		}
	}
}
