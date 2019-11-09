using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{

    public GameObject panel_help;
    public GameObject panel_infos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newGame()
    {
        SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
    }

    public void unpauseGame()
    {
        foreach (AudioSource audioSource in (FindObjectOfType<Controller>().cam.camHolder.GetComponentsInChildren<AudioSource>()))
        {
            audioSource.UnPause();
        }
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Pause").buildIndex);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void showHideInfos()
    {
        panel_infos.SetActive(!panel_infos.activeSelf);
    }

    public void showHideHelp()
    {
        panel_help.SetActive(!panel_help.activeSelf); ;
    }
}
