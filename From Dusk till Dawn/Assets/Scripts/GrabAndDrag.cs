using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndDrag : MonoBehaviour {

    [SerializeField]
    private BaseGrabbable grabbable;

    private bool isGrabbed;
    private BaseGrabber grabber;

    public GameObject glassManager;

    // Use this for initialization
    void Start () {
        if (grabbable == null)
        {
            grabbable = GetComponent<BaseGrabbable>();
        }

        grabbable.OnGrabStateChange += Grab;
        grabbable.OnContactStateChange += Grab;


        glassManager = GameObject.FindGameObjectWithTag("MoleManager");

    }

    private void Update()
    {
        if (isGrabbed)
        {
            this.transform.position = grabber.GrabHandle.position;
            this.transform.rotation = grabber.GrabHandle.rotation;
        }
    }

    // Update is called once per frame
    void Grab (BaseGrabbable baseGrab) {

        switch (baseGrab.ContactState)
        {
            case GrabStateEnum.Inactive:
                isGrabbed = false;
                break;

            case GrabStateEnum.Multi:
                //isGrabbed = true;
                //grabber = baseGrab.GrabberPrimary;
                glassManager.SendMessage("OnTriggerEnter", this.gameObject.GetComponent<Collider>());
                break;

            case GrabStateEnum.Single:
                glassManager.SendMessage("OnTriggerEnter", this.gameObject.GetComponent<Collider>());
                //isGrabbed = true;
               // grabber = baseGrab.GrabberPrimary;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

    }
}
