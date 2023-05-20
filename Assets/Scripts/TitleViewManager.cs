using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleViewManager : MonoBehaviour
{
    [SerializeField] private GameObject _titleView;
    [SerializeField] private GameObject _creditsView;

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

    private void HideView(GameObject canvas)
    {
        canvas.SetActive(false);
    }

    private void ShowView(GameObject canvas)
    {
        canvas.SetActive(true);
    }
}
