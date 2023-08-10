using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
[BurstCompile]
public partial struct PlayerMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var speed = SystemAPI.Time.DeltaTime * 4;
        var rotateSpeed = SystemAPI.Time.DeltaTime * 720;

        foreach (var (input, trans)
                 in SystemAPI.Query<RefRO<PlayerInput>, RefRW<LocalTransform>>().WithAll<Simulate>())
        {
            var moveInput = new float2(input.ValueRO.Horizontal, input.ValueRO.Vertical);
            moveInput = math.normalizesafe(moveInput) * speed;
            trans.ValueRW.Position += new float3(moveInput.x, 0, moveInput.y);
            if (math.lengthsq(moveInput) > 0)
            {
                trans.ValueRW.Rotation =
                    Quaternion.RotateTowards(
                        trans.ValueRO.Rotation,
                        quaternion.LookRotationSafe(new float3(moveInput.x, 0, moveInput.y), math.up()),
                        rotateSpeed);
                ;
            }
        }
    }
}