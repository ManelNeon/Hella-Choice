using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        if (LanguageManager.Instance.isPortuguese)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ClickPortuguese()
    {
        //put something showing its portuguese

        LanguageManager.Instance.isPortuguese = true;
    }

    public void ClickEnglish()
    {
        LanguageManager.Instance.isPortuguese = false;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
