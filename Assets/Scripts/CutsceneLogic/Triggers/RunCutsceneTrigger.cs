using PlayerSystem;
using UnityEngine;
using Zenject;

namespace CutsceneLogic
{
    public enum RunCutsceneType
    {
        RunLeft,
        RunRight
    }

    public enum StateCutsceneType
    {
        Start,
        Stop
    }

    public class RunCutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private RunCutsceneType _cutsceneType;
        [SerializeField] private StateCutsceneType _stopCutsceneType;

        private Player _player;
        private CutsceneManager _cutsceneManager;

        [Inject]
        public void Construct(Player player, CutsceneManager cutsceneManager)
        {
            _player = player;
            _cutsceneManager = cutsceneManager;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            if (_stopCutsceneType == StateCutsceneType.Stop)
            {
                _player.StopMoveCutscene();
                _cutsceneManager.EndCutscene();
            }

            if (_stopCutsceneType == StateCutsceneType.Start)
            {
                _cutsceneManager.StartCutscene();

                if (_cutsceneType == RunCutsceneType.RunLeft)
                {
                    _player.StartMoveCutscene(Vector2.left);
                }
                else if (_cutsceneType == RunCutsceneType.RunRight)
                {
                    _player.StartMoveCutscene(Vector2.right);
                }
            }
        }
    }
}