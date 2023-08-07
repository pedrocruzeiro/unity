using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    public bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
            
        }
    
    }

    void PauseGame ()
    {
        Debug.Log("Pause Log");
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame ()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadScene(string sceneName)
    {
        ResumeGame();
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
            Application.Quit();
        
    }
}
