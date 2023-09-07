using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[WorldSystemFilter(WorldSystemFilterFlags.ThinClientSimulation)]
public partial struct ThinClientInputSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NetworkId>();
    }

    public void OnUpdate(ref SystemState state)
    {
        if (SystemAPI.TryGetSingleton<CommandTarget>(out var commandTarget) &&
            commandTarget.targetEntity == Entity.Null)
        {
            CreateThinClientPlayer(ref state);
        }

        foreach (var input in SystemAPI.Query<RefRW<PlayerInput>>())
        {
            input.ValueRW.Horizontal = SystemAPI.Time.ElapsedTime % 2 > 1 ? 1 : -1;
            input.ValueRW.Vertical = (SystemAPI.Time.ElapsedTime + 0.5d) % 2 > 1 ? 1 : -1;
        }
    }

    private void CreateThinClientPlayer(ref SystemState state)
    {
        Entity ent = state.EntityManager.CreateEntity();
        state.EntityManager.AddComponent<PlayerInput>(ent);

        var connectionId = SystemAPI.GetSingleton<NetworkId>().Value;
        state.EntityManager.AddComponentData(ent, new GhostOwner() { NetworkId = connectionId });
        
        state.EntityManager.AddComponent<Assembly_CSharp.Generated.PlayerInputInputBufferData>(ent);

        SystemAPI.SetSingleton(new CommandTarget { targetEntity = ent });
    }
}