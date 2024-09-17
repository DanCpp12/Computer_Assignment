using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;


public struct Base : IComponentData
{
    public float2 SpawnPosition;
    public float ResspawnTime;
    public float NextSpawnTime;
}
