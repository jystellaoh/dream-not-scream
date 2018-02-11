using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameStateManager {

    public static bool dream1Completed = false;
    public static bool dream2Completed = false;
    public static bool dream3Completed = false;
    public static float timeUntilSunrise = 600f;

    public static void CheckState()
    {
        if (dream1Completed && dream2Completed && dream3Completed)
        {
            FadeManager.Instance.DoFade(5f, 0f, null, null);
            GameObject[] cameras = GameObject.FindGameObjectsWithTag("CameraParent");
            Debug.Log("num cameras " + cameras.Count());
            foreach (GameObject camera in cameras)
            {
                MotionControllerVisualizer vis = camera.GetComponentInChildren<MotionControllerVisualizer>();
                Debug.Log(camera.scene.buildIndex);
                if (camera.scene.buildIndex != 7)
                {
                    if (vis != null)
                    {
                        //Destroy(vis);
                        //Destroy(camera.GetComponentInChildren<MixedRealityCameraManager>());
                    }
                    camera.SetActive(false);
                    //Destroy(camera);
                }
            }
            SceneManager.LoadScene(7, LoadSceneMode.Single);
            Debug.Log("I thinik this statement is unrechable :O");

            cameras = GameObject.FindGameObjectsWithTag("CameraParent");
            Debug.Log("num cameras " + cameras.Count());
            foreach (GameObject camera in cameras)
            {
                MotionControllerVisualizer vis = camera.GetComponentInChildren<MotionControllerVisualizer>();
                Debug.Log(camera.scene.buildIndex);
                if (camera.scene.buildIndex != 7)
                {
                    if (vis != null)
                    {
                        //Destroy(vis);
                        //Destroy(camera.GetComponentInChildren<MixedRealityCameraManager>());
                    }
                    camera.SetActive(false);
                    //Destroy(camera);
                }
            }
            SceneManager.LoadScene(7);
            //Invoke("GameWon", 5f);
        }
    }

    public static void GameWon()
    {
        Time.timeScale = 0;
    }
}
