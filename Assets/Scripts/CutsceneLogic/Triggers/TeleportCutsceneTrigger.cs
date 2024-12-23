using System;
using DG.Tweening;
using PlayerSystem;
using UnityEngine;
using Zenject;

namespace CutsceneLogic.Triggers
{
    public class TeleportCutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _telepoortDelay;

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
                DOVirtual.DelayedCall(_telepoortDelay, TeleportPlayer);
            }
        }

        private void TeleportPlayer()
        {
            _player.transform.position = _target.position;
        }
    }
}