using System;
using Input.Readers;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PauseManager : MonoBehaviour
    {
        private MenuInputReader _menuInputReader;

        [Inject]
        public void Construct(MenuInputReader menuInputReader)
        {
            _menuInputReader = menuInputReader;
        }

        private void OnEnable()
        {
            _menuInputReader.OnQuitTriggered += ClosePause;
        }

        private void OnDisable()
        {
            _menuInputReader.OnQuitTriggered -= ClosePause;
        }

        private void ClosePause()
        {
            Debug.Log("blabla");
        }
    }
}