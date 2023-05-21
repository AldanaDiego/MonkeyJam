using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitManager : MonoBehaviour
{
    public void QuitGame()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
