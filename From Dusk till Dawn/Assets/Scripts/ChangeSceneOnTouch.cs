// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HoloToolkit.Unity.InputModule.Examples.Grabbables
{
    /// <summary>
    /// Simple class to change the color of grabbable objects based on state
    /// </summary>
    public class ChangeSceneOnTouch : MonoBehaviour
    {

        [SerializeField]
        private BaseGrabbable grabbable;

        public float fadeout; //how long it takes to fade into the next scene

        public int sceneToChangeTo = 0;
        
        private void Awake()
        {
            if (grabbable == null)
            {
                grabbable = GetComponent<BaseGrabbable>();
            }
          
            grabbable.OnContactStateChange += ChangeScene;
            grabbable.OnGrabStateChange += ChangeScene;
        }

        private void ChangeScene(BaseGrabbable baseGrab)
        {

            Debug.Log(baseGrab.ContactState);

            switch (baseGrab.ContactState)
            {
                case GrabStateEnum.Inactive:
                    break;

                case GrabStateEnum.Multi:
                    SceneTransition();
                    break;

                case GrabStateEnum.Single:
                    SceneTransition();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (baseGrab.GrabState)
            {
                case GrabStateEnum.Inactive:
                    break;

                case GrabStateEnum.Multi:
                    SceneTransition();
                    break;

                case GrabStateEnum.Single:
                    SceneTransition();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }


        }

        public void SceneTransition()
        {
            FadeManager.Instance.DoFade(fadeout, 0f, null, null);
            Invoke("ChangeScene", fadeout);
        }

        public void ChangeScene()
        {
            GameObject[] cameras = GameObject.FindGameObjectsWithTag("CameraParent");
            Debug.Log("num cameras " + cameras.Count());
            foreach (GameObject camera in cameras)
            {
                MotionControllerVisualizer vis = camera.GetComponentInChildren<MotionControllerVisualizer>();
                Debug.Log(camera.scene.buildIndex);
                if (camera.scene.buildIndex != sceneToChangeTo)
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
            SceneManager.LoadScene(sceneToChangeTo, LoadSceneMode.Single);
            Debug.Log("I thinik this statement is unrechable :O");

            cameras = GameObject.FindGameObjectsWithTag("CameraParent");
            Debug.Log("num cameras " + cameras.Count());
            foreach (GameObject camera in cameras)
            {
                MotionControllerVisualizer vis = camera.GetComponentInChildren<MotionControllerVisualizer>();
                Debug.Log(camera.scene.buildIndex);
                if (camera.scene.buildIndex != sceneToChangeTo)
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
        }
    }
}
