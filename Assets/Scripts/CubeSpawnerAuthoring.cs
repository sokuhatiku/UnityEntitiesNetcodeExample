using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct CubeSpawner : IComponentData
{
    public Entity Cube;
}

[DisallowMultipleComponent]
public class CubeSpawnerAuthoring : MonoBehaviour
{
    public GameObject Cube;

    class Baker : Baker<CubeSpawnerAuthoring>
    {
        public override void Bake(CubeSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var component = default(CubeSpawner);
            component.Cube = GetEntity(authoring.Cube, TransformUsageFlags.None);
            AddComponent(entity, component);
        }
    }
}