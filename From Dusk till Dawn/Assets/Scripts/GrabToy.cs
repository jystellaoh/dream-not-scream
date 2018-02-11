using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabToy : MonoBehaviour {

    [SerializeField]
    private BaseGrabbable grabbable;

    private GameObject momManager;

    // Use this for initialization
    void Start () {

        if (grabbable == null)
        {
            grabbable = GetComponent<BaseGrabbable>();
        }
        momManager = GameObject.FindGameObjectWithTag("MoleManager");
        grabbable.OnContactStateChange += Grab;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Grab(BaseGrabbable baseGrab)
    {
        switch (baseGrab.ContactState)
        {
            case GrabStateEnum.Inactive:
                break;

            case GrabStateEnum.Multi:
                momManager.SendMessage("CheckCurrentToy", this.gameObject);
                break;

            case GrabStateEnum.Single:
                momManager.SendMessage("CheckCurrentToy", this.gameObject);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
