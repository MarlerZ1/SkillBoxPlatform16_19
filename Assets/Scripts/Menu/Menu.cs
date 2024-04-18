using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[DisallowMultipleComponent]
public class Menu : MonoBehaviour
{
    [SerializeField] GameObject[] onPauseActivateElements;
    [SerializeField] GameObject[] onPauseDeactivateElements;


    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void ChangePauseState()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            for (int i = 0; i < onPauseDeactivateElements.Length; i++)
            {
                onPauseDeactivateElements[i].SetActive(false);
            }
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            for (int i = 0; i < onPauseActivateElements.Length; i++)
            {
                onPauseActivateElements[i].SetActive(true);
            }
        }
    }



    public void LoadMainMenu()
    {
        LoadLvl(0);
    }

    public void ReloadCurrentLvl() 
    {
        LoadLvl(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLvl()
    {
        LoadLvl(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool LoadNextLvlBool()
    {
        return LoadLvl(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public bool CanLoadNextLvl()
    {
        return CanLoadLvl(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool CanLoadLvl(int lvlNumber)
    {
        if (lvlNumber > SceneManager.sceneCount)
            return false;
        return true;
    }

    public bool LoadLvl(int lvlNumber)
    {
        if (!CanLoadLvl(lvlNumber))
            return false;

        SceneManager.LoadScene(lvlNumber);
        Time.timeScale= 1;
        return true;
    }
}
