using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void StartGame ()
    {
        // loads next level by returning index of current scene + 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        
    }

    public void TryAgain ()
    {
        // loads previous level by returning index of current scene + 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void QuitGame ()
    {
        Debug.Log("Game was quit!");
        Application.Quit();
    }
}
