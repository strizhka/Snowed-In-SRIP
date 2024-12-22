using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class PauseButton : Button
    {
        private Tween _tween;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            _tween?.Kill();
            _tween = transform.DOScale(1.1f, 0.2f);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            _tween?.Kill();
            _tween = transform.DOScale(1f, 0.2f);
        }
    }
}