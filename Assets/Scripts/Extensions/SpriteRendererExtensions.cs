using UnityEngine;
using DG.Tweening;

namespace Tetris.Extensions
{
    public static class SpriteRendererExtensions
    {
        public static void FadeDestroy(this SpriteRenderer spriteRenderer, float fadeTime)
        {
            spriteRenderer.DOFade(0f, fadeTime).OnComplete(() =>
            {
                Object.Destroy(spriteRenderer);
            });
        }

        /*
        public static void FadeDestroy(this SpriteRenderer spriteRenderer, float fadeTime, Color fadeColor)
        {
            float stepTime = fadeTime / 2;
            DOTween.Sequence()
                .Append(spriteRenderer.DOColor(fadeColor, stepTime))
                .Append(spriteRenderer.DOFade(0f, stepTime));
        }
        */
    }
}

