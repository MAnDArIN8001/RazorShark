using UnityEngine;
using Zenject;

public class PlayerConfigInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerCharacteristics>().FromResources(PlayerResourcesConsts.PlayerConfigPath);
    }
}
