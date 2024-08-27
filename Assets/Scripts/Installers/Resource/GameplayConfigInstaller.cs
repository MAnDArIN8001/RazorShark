using UnityEngine;
using Zenject;

public class GameplayConfigInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SoundEffectsConfig>().FromResources(GameplayConsts.SoundConfigPath);
    }
}
