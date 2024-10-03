using Cinemachine;
using Input.Readers;
using PlayerSystem;
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
    private Player _player;
    private bool _isFalling;

    [Inject]
    private void Construct(GameplayInputReader inputReader, GameStateMachine gameStateMachine, Player player)
    {
        _gameplayInputReader = inputReader;
        _gameStateMachine = gameStateMachine;
        _player = player;
    }

    private void OnEnable()
    {
        _isFalling = false;
    }

    private void Update()
    {
        Switch();
    }

    private void Switch()
    {
        if (_player.LastOnGroundTime < -1 && _player.Rb.velocity.y < -1)
        {
            CameraManager.SwitchCamera(cam1);
            _isFalling = true;
        }
        if (_player.LastOnGroundTime > 0 && _player.Rb.velocity.y > -1 && _isFalling)
        {
            CameraManager.SwitchCamera(cam2);
            _isFalling = false;
        }
    }
}
