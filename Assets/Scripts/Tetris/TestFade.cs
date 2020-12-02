using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestFade : MonoBehaviour
{
    private SpriteRenderer[] _renderers;

    private void Start()
    {
        _renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartFading();
        }
    }

    private void StartFading()
    {
        foreach (var renderer in _renderers)
        {
            renderer.DOColor(Color.red, 0.3f);
            renderer.DOFade(0, 0.5f);
        }
    }
}
