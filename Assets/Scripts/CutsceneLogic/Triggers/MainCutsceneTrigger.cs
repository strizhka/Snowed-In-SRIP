using DG.Tweening;
using PlayerSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CutsceneLogic
{
    public class MainCutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private Sprite[] _images;
        [SerializeField] private Image _cutsceneImage;
        [SerializeField] private float _cutsceneDuration;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private Transform _endCutscenePosition;

        private CutsceneManager _cutsceneManager;
        private Player _player;

        [Inject]
        public void Construct(CutsceneManager cutsceneManager, Player player)
        {
            _cutsceneManager = cutsceneManager;
            _player = player;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _cutsceneManager.StartCutscene();
                StartMainCutscene();
            }
        }

        private void StartMainCutscene()
        {
            Sequence sequence = DOTween.Sequence();

            _cutsceneImage.gameObject.SetActive(true);

            for (int i = 0; i < _images.Length; i++)
            {
                var i1 = i;
                sequence.AppendCallback(() => _cutsceneImage.sprite = _images[i1]);
                sequence.AppendInterval(_fadeDuration);
            }

            sequence.AppendInterval(_cutsceneDuration);
            sequence.AppendCallback(() => _cutsceneManager.EndCutscene());
            sequence.AppendCallback(() => _cutsceneImage.gameObject.SetActive(false));
            sequence.AppendCallback(() => _player.transform.position = _endCutscenePosition.position);

            sequence.Play();
        }
    }
}