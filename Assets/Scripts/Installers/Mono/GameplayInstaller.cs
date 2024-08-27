using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private SceneManagment _sceneManagment;

    public override void InstallBindings()
    {
        Container.Bind<SceneManagment>().FromInstance(_sceneManagment);
        Container.Bind<MoneyController>().FromNew().AsSingle();
    }
}
