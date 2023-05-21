using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleViewManager : MonoBehaviour
{
    [SerializeField] private GameObject _titleView;
    [SerializeField] private GameObject _creditsView;
    [SerializeField] private GameObject _storyView;

    public void ShowCreditsView()
    {
        HideView(_titleView);
        HideView(_storyView);
        ShowView(_creditsView);
    }

    public void ShowTitleView()
    {
        HideView(_creditsView);
        HideView(_storyView);
        ShowView(_titleView);
    }

    public void ShowStoryView()
    {
        HideView(_creditsView);
        HideView(_titleView);
        ShowView(_storyView);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
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
