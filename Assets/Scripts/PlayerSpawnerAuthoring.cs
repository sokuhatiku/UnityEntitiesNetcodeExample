using Unity.Entities;
using UnityEngine;

public struct PlayerSpawner : IComponentData
{
    public Entity PlayerPrefab;
}

[DisallowMultipleComponent]
public sealed class PlayerSpawnerAuthoring : MonoBehaviour
{
    public GameObject playerPrefab;

    private sealed class Baker : Baker<PlayerSpawnerAuthoring>
    {
        public override void Bake(PlayerSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            var component = default(PlayerSpawner);
            component.PlayerPrefab = GetEntity(authoring.playerPrefab, TransformUsageFlags.None);
            AddComponent(entity, component);
        }
    }
}