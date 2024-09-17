using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct BaseSystem : ISystem
{
    private Entity PlayerEntity;
    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRW<Base> Base in SystemAPI.Query<RefRW<Base>>())
        {
			PlayerEntity = getPlayer(ref state);
			if (PlayerEntity != Entity.Null && state.EntityManager.GetComponentData<LocalTransform>(PlayerEntity).Position.y <= -9000)
            {
                if (Base.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
                {
                    float3 pos = new float3(Base.ValueRO.SpawnPosition.x, Base.ValueRO.SpawnPosition.y, 0);
					state.EntityManager.SetComponentData(PlayerEntity, LocalTransform.FromPosition(pos));
                }
            }
            else
            {
				Base.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + Base.ValueRO.ResspawnTime;
			}
        }
    }
    private Entity getPlayer(ref SystemState state)
    {
        Entity player = Entity.Null;
		NativeArray<Entity> entities = state.EntityManager.GetAllEntities(Allocator.Temp);
		foreach (Entity entity in entities)
        {
            if (state.EntityManager.HasComponent<PlayerTag>(entity) && state.EntityManager.HasComponent<Player>(entity))
            {
                player = entity;
            }
        }
        entities.Dispose();
        return player;
    }
}
