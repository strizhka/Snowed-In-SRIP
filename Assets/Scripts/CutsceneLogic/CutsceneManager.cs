using UnityEngine;
using Zenject;

namespace CutsceneLogic
{
    public class CutsceneManager
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void StartCutscene()
        {
            _gameStateMachine.ChangeState(GameState.Cutscene);
        }

        public void EndCutscene()
        {
            _gameStateMachine.ChangeState(GameState.Gameplay);
        }
    }
}