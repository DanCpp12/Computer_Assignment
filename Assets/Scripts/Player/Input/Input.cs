using Unity.Entities;
using Unity.Mathematics;

public struct Input : IComponentData
{
    public float2 Movement;
    public float2 MousePos;
}
