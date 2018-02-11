
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;
using UnityEngine.XR;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;

public class FlyingScript : MonoBehaviour {

    public float speed = 10f;
    public float xMax = -2000f;
    public float xMin = -7500f;
    public float yMin = -500f;
    public float yMax = 900f;
    public float zMin = 25f;
    public float zMax = 4500f;

    private bool isFlying = false;
    private Rigidbody rb;
    private Transform newTransform;

    private GameObject camera;

    private XRNode flyingHand;
    private InteractionSourceHandedness handedness = InteractionSourceHandedness.Left;

    private InteractionSourcePressType pressType = InteractionSourcePressType.Select;

    // Use this for initialization
    void Start()
    {
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
        InteractionManager.InteractionSourceReleased += InteractionSourceReleased;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        newTransform = new GameObject().transform;
    }

    void Update()
    {
        Vector3 dir = InputTracking.GetLocalPosition(XRNode.LeftHand);
        Quaternion rotate = InputTracking.GetLocalRotation(XRNode.LeftHand);
        
        newTransform.position = dir;
        newTransform.rotation = rotate;

        if (isFlying)
        {
            Vector3 oldPosition = this.transform.position;
            Vector3 newDir = (newTransform.forward * speed); 
            Vector3 newPosition = new Vector3(Mathf.Clamp(oldPosition.x + newDir.x, xMin, xMax), Mathf.Clamp(oldPosition.y + newDir.y, yMin, yMax), Mathf.Clamp(oldPosition.z + newDir.z, zMin, zMax));
            this.transform.position += newDir;//= newPosition;

        }
    }
    // Update is called once per frame
    private void InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
    {
        //Debug.Log(obj.pressType);
        if (obj.pressType == pressType) // && obj.state.source.handedness == handedness)
        {
            isFlying = true;     
        }
    }

    private void InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
    {
        // g.Log(obj.pressType);
        if (obj.pressType == pressType) // && obj.state.source.handedness == handedness)
        {
            isFlying = false;
        }
    }
}
