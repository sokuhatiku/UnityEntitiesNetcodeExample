using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using UnityEngine;

public struct ThinClientMoveTarget : IComponentData
{
    public float2 TargetPosition;
    public float NextUpdateTime;
}

public sealed class ThinClientMoveTargetAuthoring : MonoBehaviour
{
    public sealed class Baker : Baker<ThinClientMoveTargetAuthoring>
    {
        public override void Bake(ThinClientMoveTargetAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            var component = default(ThinClientMoveTarget);
            component.TargetPosition = float2.zero;
            component.NextUpdateTime = 0;
            AddComponent(entity, component);
        }
    }
}