using UnityEngine;
using DG.Tweening;

namespace Tetris.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class UI_Screen : MonoBehaviour
    {
        public enum AnimationType
        {
            Fade, Collapse
        }

        [SerializeField] private AnimationType _animationType = default;
        
        [SerializeField] private float _transitionTime = 0.2f;
        [SerializeField] private bool _isHiddenByDefault = true;
        
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        
        private Tween _tweener;
        
        private readonly Vector3 _hiddenScale = new Vector3(0f, 1f, 1f);
        private readonly Vector3 _activeScale = new Vector3(1f, 1f, 1f);

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetupScreen()
        {
            this.gameObject.SetActive(true);

            _canvasGroup.blocksRaycasts = !_isHiddenByDefault;
            
            switch (_animationType)
            {
                case AnimationType.Collapse:
                    _rectTransform.localScale = _isHiddenByDefault ? _hiddenScale : _activeScale;
                    break;
                case AnimationType.Fade:
                    _canvasGroup.alpha = _isHiddenByDefault ? 0 : 1;
                    break;
            }
        }

        [ContextMenu("Hide")]
        public void Hide()
        {
            switch (_animationType)
            {
                case AnimationType.Collapse:
                    HideCollapse();
                    break;
                case AnimationType.Fade:
                    HideFade();
                    break;
            }
        }

        [ContextMenu("Show")]
        public void Show()
        {
            switch (_animationType)
            {
                case AnimationType.Collapse:
                    ShowCollapse();
                    break;
                case AnimationType.Fade:
                    ShowFade();
                    break;
            }
        }

        private void ShowCollapse()
        {
            _rectTransform.DOScale(_activeScale, _transitionTime).OnStart(() =>
            {
                _canvasGroup.alpha = 1;
                _canvasGroup.blocksRaycasts = true;
            });
        }
        
        private void HideCollapse()
        {
            _tweener = _rectTransform.DOScale(_hiddenScale, _transitionTime).OnComplete(() =>
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.blocksRaycasts = false;
            });
        }

        private void ShowFade()
        {
            _tweener = _canvasGroup.DOFade(1, _transitionTime).OnComplete(() =>
            {
                _canvasGroup.alpha = 1;
                _canvasGroup.blocksRaycasts = true;
            });
        }
        
        private void HideFade()
        {
            _tweener = _canvasGroup.DOFade(0, _transitionTime).OnComplete(() =>
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.blocksRaycasts = false;
            });
        }
    }
}
