using System;
using System.Collections.Generic;
using Input.Readers;
using UnityEngine;

namespace Input
{
    public class InputReaderSwitcher : MonoBehaviour
    {
        [SerializeField] private List<BaseInputReader> _inputHandlers = new();
        private BaseInputReader _activeInputReader;

        public List<BaseInputReader> InputHandlers => _inputHandlers;

        private void Update()
        {
            LogActiveReaders();
        }

        public void SetActiveInputHandler(InputHandlerType inputHandlerType)
        {
            foreach (var inputHandler in _inputHandlers)
            {
                if (inputHandler.InputHandler == inputHandlerType)
                {
                    inputHandler.EnableInput();
                }
                else
                {
                    inputHandler.DisableInput();
                }
            }
            
            _activeInputReader = _inputHandlers.Find(x => x.InputHandler == inputHandlerType);
            
            if (_activeInputReader == null)
            {
                Debug.LogError("Active Input Reader is null!");
            }
        }

        public void DisableAllInput()
        {
            foreach (var handler in _inputHandlers)
            {
                handler.DisableInput();
            }
        }

        public void EnableInputReader(InputHandlerType inputHandlerType)
        {
            var inputReader = _inputHandlers.Find(x => x.InputHandler == inputHandlerType);
            inputReader.EnableInput();
        }

        public void DisableInputReader(InputHandlerType inputHandlerType)
        {
            var inputReader = _inputHandlers.Find(x => x.InputHandler == inputHandlerType);
            inputReader.DisableInput();
        }

        #region LogActiveReaders

        private void LogActiveReaders()
        {
            Debug.Log("active readers:");
            foreach (var inputHandler in _inputHandlers)
            {
                Debug.Log(inputHandler.IsInputEnabled() + " " + inputHandler.InputHandler + " " + "reader");
            }
        }

        private bool IsInputEnabled(InputHandlerType inputHandlerType)
        {
            var inputReader = _inputHandlers.Find(x => x.InputHandler == inputHandlerType);
            return inputReader.IsInputEnabled();
        }

        #endregion
    }
}