using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Quaternion _playerInstanceRotation;

    [SerializeField] private Transform _playerInstancePoint;

    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;

    public override void InstallBindings()
    {
        GameObject playerInstance = Container.InstantiatePrefab(_playerPrefab, _playerInstancePoint.position, _playerInstanceRotation, null);

        Container.Bind<PlayerHealth>().FromInstance(playerInstance.GetComponent<PlayerHealth>());
        Container.Bind<Transform>().FromInstance(playerInstance.transform);

        _mainVirtualCamera.Follow = playerInstance.transform;
    }
}
