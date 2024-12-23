using PlayerSystem;
using UnityEngine;
using Zenject;

namespace CutsceneLogic
{
    public class JumpCutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private float _jumpForce;

        private Player _player;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _player.CutsceneJump(_jumpForce);
            }
        }
    }
}