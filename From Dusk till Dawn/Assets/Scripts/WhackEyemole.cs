// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using HoloToolkit.Unity.InputModule.Examples.Grabbables;

    public class WhackEyemole : MonoBehaviour
    {

        [SerializeField]
        private BaseGrabbable grabbable;
        [SerializeField]
        public AudioSource deathSound;
        [SerializeField]
        private AudioSource phrase = null;
        private GameObject moleManager;

        private float minY = -5f; 
        private float baseY = 0.2f;

        private bool isDying = false;

        private float timeSinceLastPhrase = 0f;
        private float timeBetweenPhrases = 5f;
        
        private void Awake()
        {
            if (grabbable == null)
            {
                grabbable = GetComponent<BaseGrabbable>();
            }
            moleManager = GameObject.FindGameObjectWithTag("MoleManager");
            grabbable.OnContactStateChange += Whack;
        }

        public void SetPhrase(AudioSource phr) {
            this.phrase = phr;
        }

        private void Update() {

            Vector3 molePosition = gameObject.transform.position;

            if (isDying) {
                molePosition.y -= 0.1f;
                gameObject.transform.position = molePosition;
            } 
            else if (Time.timeSinceLevelLoad - timeSinceLastPhrase >= timeBetweenPhrases) {
                phrase.Play();
                timeSinceLastPhrase = Time.timeSinceLevelLoad;
            }

            if (gameObject.transform.position.y <= minY)
            {
                moleManager.SendMessage("CheckIfDone");
                Destroy(gameObject);
            }

            if (gameObject.transform.position.y <= baseY) {
                molePosition += Vector3.up * 0.01f;
                gameObject.transform.position = molePosition;
            }


        }

        private void Whack(BaseGrabbable baseGrab)
        {

            switch (baseGrab.ContactState)
            {
                case GrabStateEnum.Inactive:
                    break;

                case GrabStateEnum.Multi:
                    isDying = true;
                    deathSound.Play();
                    break;

                case GrabStateEnum.Single:
                    isDying = true;
                    deathSound.Play();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }


        }
    }

