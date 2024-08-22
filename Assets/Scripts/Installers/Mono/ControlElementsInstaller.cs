using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ControlElementsInstaller : MonoInstaller
{
    [SerializeField] private Joystick _joystick;

    [SerializeField] private Button _attackButton;

    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromInstance(_joystick);
        Container.Bind<Button>().WithId(ControlsButton.Attack).FromInstance(_attackButton);
        
    }
}
