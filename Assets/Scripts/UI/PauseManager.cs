using System;
using InputLogic.Readers;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace UI
{
    public class PauseManager : MonoBehaviour
    {
        private MenuInputReader _menuInputReader;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(MenuInputReader menuInputReader, GameStateMachine gameStateMachine)
        {
            _menuInputReader = menuInputReader;
            _gameStateMachine = gameStateMachine;
        }

        private void OnEnable()
        {
            _gameStateMachine.ChangeState(GameState.Menu);
            _menuInputReader.OnQuitTriggered += ClosePause;
            _menuInputReader.OnQuitTriggered += Resume;
            _menuInputReader.OnQuitTriggered += Settings;
            _menuInputReader.OnQuitTriggered += Exit;
        }

        private void OnDisable()
        {
            _gameStateMachine.ChangeState(GameState.Gameplay);
            _menuInputReader.OnQuitTriggered -= ClosePause;
            _menuInputReader.OnQuitTriggered -= Resume;
            _menuInputReader.OnQuitTriggered -= Settings;
            _menuInputReader.OnQuitTriggered -= Exit;
        }

        private void ClosePause()
        {
            gameObject.SetActive(false);
        }

        public void Resume()
        {
            ClosePause();
        }

        public void Settings()
        {

        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}