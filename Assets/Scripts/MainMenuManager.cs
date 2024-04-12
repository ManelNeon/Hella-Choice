using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject languageMenu;

    private void Start()
    {
        languageMenu.SetActive(false);
    }

    public void Play()
    {
        languageMenu.SetActive(true);
    }

    public void ClickPortuguese()
    {
        LanguageManager.Instance.isPortuguese = true;

        SceneManager.LoadScene(1);
    }

    public void ClickEnglish()
    {
        LanguageManager.Instance.isPortuguese = false;

        SceneManager.LoadScene(2);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
