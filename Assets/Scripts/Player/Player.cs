using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Player : IComponentData
{
	public Entity Prefab;
	public float MoveSpeed;
}

public struct PlayerTag : IComponentData { }