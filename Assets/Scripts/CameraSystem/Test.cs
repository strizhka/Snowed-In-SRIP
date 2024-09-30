using Cinemachine;
using Input.Readers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera cam1;
    [SerializeField]public CinemachineVirtualCamera cam2;

    private GameplayInputReader _gameplayInputReader;
    private GameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(GameplayInputReader inputReader, GameStateMachine gameStateMachine)
    {
        _gameplayInputReader = inputReader;
        _gameStateMachine = gameStateMachine;
    }

    private void OnEnable()
    {
        //_gameplayInputReader.OnJumpTriggered += Switch;
    }

    private void Switch()
    {
        if (CameraManager.ActiveCamera == cam1)
        {
            CameraManager.SwitchCamera(cam2);
        }
        else
        {
            CameraManager.SwitchCamera(cam1);
        }
    }
}
