using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tetris.Controllers;
using TMPro;
using UnityEngine;

namespace Tetris.Views
{
    public class PrepareView : BaseView
    {
        [SerializeField] private TMP_Text _pressAnyKeyTip = default;
        
        public override void Setup(GameController gameController)
        {
            base.Setup(gameController);
            _pressAnyKeyTip.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            DOTween.Kill(_pressAnyKeyTip);
        }
    }
}

