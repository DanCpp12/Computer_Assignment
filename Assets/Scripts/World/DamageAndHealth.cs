using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct DamageAndHealth : IComponentData
{
    public float Health;
    public float Damage;
}
