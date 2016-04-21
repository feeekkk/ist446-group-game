using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {
    
    public bool isEnabled;
    public GameObject pauseMenu;
    public static GameObject savedMenu; 

    void Start ()
    {
        savedMenu = pauseMenu;
        isEnabled = false;
        pauseMenu.SetActive (false);
    }

    void Update()
    {        
        enableMenu ();        
    }

    void enableMenu(){
        if (Input.GetButtonDown ("Fire3")) {
            if (!isEnabled) {
                pauseMenu = savedMenu;
                Time.timeScale = 0.0f;
                isEnabled = true;
                pauseMenu.SetActive (true);
            } else if (isEnabled) {
                Time.timeScale = 1.0f;
                isEnabled = false;
                pauseMenu.SetActive(false);
            }
        }    
    }

    public void ResumeGame(){
        Time.timeScale = 1.0f;
        isEnabled = false;
        pauseMenu.SetActive(false);
    }

    public void GoToMainMenu(){
        Application.LoadLevel ("NetworkSelect");
    }

    public void ExitGame(){
        Application.Quit();
    }
        
}