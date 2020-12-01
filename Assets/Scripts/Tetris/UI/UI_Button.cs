using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Tetris.UI
{
    [RequireComponent(typeof(Image))]
    public class UI_Button : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _transitionTime = 0.2f;
    
        [SerializeField] private Color _onMouseEnterColor = Color.white;
        [SerializeField] private Color _onMouseClickColor = Color.white;

        private Image _buttonImage;
        private Color _defaultColor;
        private Tween _tweener;
    
        public UnityEvent OnClick;

        private void Awake()
        {
            _buttonImage = GetComponent<Image>();
            _defaultColor = _buttonImage.color;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _tweener = DOTween.Sequence()
                .Append(_buttonImage.DOColor(_onMouseClickColor, _transitionTime))
                .Append(_buttonImage.DOColor(_defaultColor, _transitionTime));
            
            OnClick?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tweener = _buttonImage.DOColor(_onMouseEnterColor, _transitionTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tweener = _buttonImage.DOColor(_defaultColor, _transitionTime);
        }
    }
}
