using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleViewManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _titleView;
    [SerializeField] private CanvasGroup _creditsView;

    public void ShowCreditsView()
    {
        HideView(_titleView);
        ShowView(_creditsView);
    }

    public void ShowTitleView()
    {
        HideView(_creditsView);
        ShowView(_titleView);
    }

    private void HideView(CanvasGroup canvas)
    {
        canvas.alpha = 0f;
        canvas.interactable = false;
    }

    private void ShowView(CanvasGroup canvas)
    {
        canvas.alpha = 1f;
        canvas.interactable = true;
    }
}
