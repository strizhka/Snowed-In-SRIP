using System;
using DG.Tweening;
using UnityEngine;

namespace CutsceneLogic.Triggers
{
    public enum FadeType
    {
        FadeIn,
        FadeOut
    }
    public class FadeCutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _fadeCanvasGroup;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private FadeType _fadeType;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                switch (_fadeType)
                {
                    case FadeType.FadeIn:
                        FadeIn();
                        break;
                    case FadeType.FadeOut:
                        FadeOut();
                        break;
                }
            }
        }

        private void FadeIn()
        {
            _fadeCanvasGroup.gameObject.SetActive(true);
            _fadeCanvasGroup.DOFade(1, _fadeDuration);
        }

        private void FadeOut()
        {
            _fadeCanvasGroup.DOFade(0, _fadeDuration);
            _fadeCanvasGroup.gameObject.SetActive(false);
        }
    }
}