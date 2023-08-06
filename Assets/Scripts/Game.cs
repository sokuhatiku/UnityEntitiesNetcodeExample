using Unity.NetCode;
using UnityEngine;

[UnityEngine.Scripting.Preserve]
public class GameBootstrap : ClientServerBootstrap
{
    public override bool Initialize(string defaultWorldName)
    {
        Debug.Log("Bootstrap Initialize");
        AutoConnectPort = 7979;
        return base.Initialize(defaultWorldName);
    }
}