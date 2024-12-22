using System;
using InputLogic.Readers;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject _pauseMenu;

        private GameplayInputReader _gameplayInputReader;

        [Inject]
        public void Construct(GameplayInputReader gameplayInputReader)
        {
            _gameplayInputReader = gameplayInputReader;
        }

        private void OnEnable()
        {
            _gameplayInputReader.OnPauseTriggered += OpenPause;
        }

        private void OnDisable()
        {
            _gameplayInputReader.OnPauseTriggered -= OpenPause;
        }

        private void OpenPause()
        {
            _pauseMenu.SetActive(true);
        }
    }
}
