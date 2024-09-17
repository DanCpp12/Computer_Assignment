using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
	public float Health;
	public float Damage;
	public float MoveSpeed;

	public GameObject ProjectilePrefab;

	class PlayerBaker : Baker<PlayerAuthoring>
	{
		public override void Bake(PlayerAuthoring authoring)
		{
			Entity PlayerEntety = GetEntity(TransformUsageFlags.Dynamic);

			AddComponent<PlayerTag>(PlayerEntety);
			//AddComponent<Input>(PlayerEntety);
			AddComponent(PlayerEntety, new Player
			{
				MoveSpeed = authoring.MoveSpeed,
			});
			AddComponent(PlayerEntety, new DamageAndHealth
			{
				Health = authoring.Health,
				Damage = authoring.Damage,
			});

			AddComponent<FireProjectileTag>(PlayerEntety);
			SetComponentEnabled<FireProjectileTag>(PlayerEntety, false);

			AddComponent(PlayerEntety, new Projectile
			{
				Prefab = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic),
			});
		}
	}
}