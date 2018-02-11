// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using HoloToolkit.Unity.InputModule;

public class EyemoleManager : MonoBehaviour
{

    [SerializeField]
    public AudioSource[] phrases;
    [SerializeField]
    public AudioSource winningAudio;
    [SerializeField]
    public AudioSource failAudio;
    [SerializeField]
    public GameObject mole;

    public float spawnRadius;

    public GameObject camera;
    public GameObject timerText;
    private Text txt;

    private float timeSinceLastSpawn = 0f;
    private float timeBetweenSpawns = 10f;

    public float timeOut = 60f;

    private bool isSpawning = true;

    public void Awake()
    {
        txt = timerText.GetComponentInChildren<Text>();
    }


    private void Update()
    {
        if (isSpawning)
        {

            if ((Time.time - timeSinceLastSpawn >= timeBetweenSpawns))
            {
                SpawnEyemole();
                timeSinceLastSpawn = Time.time;
            }

            float timeRemaining = Mathf.Clamp(timeOut - Time.timeSinceLevelLoad , 0f, 100000f);
            string displayTxt = Math.Floor(timeRemaining / 60).ToString() + " : " + (((int)timeRemaining) % 60).ToString();
            txt.text = displayTxt;
            if (timeRemaining <= 0)
            {
                isSpawning = false;
                failAudio.Play();
                FadeManager.Instance.DoFade(5f, 0f, null, null);
                Invoke("GoBackToMain", 5);
            }
        }
    }


    public void CheckIfDone()
    {
        UnityEngine.Object[] eyemoles = FindObjectsOfType(typeof(WhackEyemole));
        Debug.Log(eyemoles.Count());
        if (eyemoles.Count() == 1)
        {
            isSpawning = false;
            winningAudio.Play();
            GameStateManager.dream2Completed = true;
            GameStateManager.CheckState();
            FadeManager.Instance.DoFade(5f, 0f, null, null);
            Invoke("GoBackToMain", 5);
        }
    }

    private void GoBackToMain()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("CameraParent");
        Debug.Log("num cameras " + cameras.Count());
        foreach (GameObject camera in cameras)
        {
            MotionControllerVisualizer vis = camera.GetComponentInChildren<MotionControllerVisualizer>();
            Debug.Log(camera.scene.buildIndex);
            if (camera.scene.buildIndex != 5)
            {
                if (vis != null)
                {
                    Destroy(vis);
                    Destroy(camera.GetComponentInChildren<MixedRealityCameraManager>());
                }
                camera.SetActive(false);
                //Destroy(camera);
            }
        }
        SceneManager.LoadScene(5);
    }

    private void SpawnEyemole()
    {

        GameObject eyemoleObj = Instantiate(mole);
        System.Random rndX = new System.Random();
        System.Random rndY = new System.Random();
        mole.transform.position = camera.transform.position + new Vector3(Mathf.Sin(rndX.Next(0, 360) * 2 * Mathf.PI / 180), 0, Mathf.Cos(rndY.Next(0, 360) * 2 * Mathf.PI / 180)) * spawnRadius;
        System.Random rndP = new System.Random();
        AudioSource randomPhrase = phrases[(int)rndP.Next(0, phrases.Count())];
        mole.GetComponent<WhackEyemole>().SetPhrase(randomPhrase);
    }
}