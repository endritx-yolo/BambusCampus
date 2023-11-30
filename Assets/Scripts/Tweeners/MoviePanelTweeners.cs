using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class MoviePanelTweeners : BaseTweener
{
    [SerializeField] private Transform _topPanelTransform;
    [SerializeField] private Transform _bottomPanelTransform;

    [BoxGroup("Tweening"), SerializeField] private float _duration = 0.4f;
    [BoxGroup("Tweening"), SerializeField] private float _delay = 1f;
    [BoxGroup("Tweening"), SerializeField] private float _yOffset = 140f;
    [BoxGroup("Tweening"), SerializeField] private Ease _ease = Ease.OutSine;

    private Tween _topPanelTween;
    private Tween _bottomPanelTween;

    private void Start()
    {
        float topPanelYDestinationPosition = _topPanelTransform.transform.localPosition.y - _yOffset;
        float bottomPanelYDestinationPosition = _bottomPanelTransform.transform.localPosition.y + _yOffset;
        _topPanelTween = _topPanelTransform.DOLocalMoveY(topPanelYDestinationPosition, _duration).SetEase(_ease);
        _topPanelTween.Pause();
        _topPanelTween.SetAutoKill(false);
        _topPanelTween.SetDelay(_delay);

        _bottomPanelTween = _bottomPanelTransform.DOLocalMoveY(bottomPanelYDestinationPosition, _duration)
            .SetEase(_ease);
        _bottomPanelTween.Pause();
        _bottomPanelTween.SetAutoKill(false);
        _bottomPanelTween.SetDelay(_delay);
    }

    public override void ExecuteTween()
    {
        _topPanelTween.PlayForward();
        _bottomPanelTween.PlayForward();
    }

    public void RevertTween()
    {
        _topPanelTween.PlayBackwards();
        _bottomPanelTween.PlayBackwards();
    }
}