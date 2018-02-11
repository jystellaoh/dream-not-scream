using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class FileSwitcher : MonoBehaviour {

    [SerializeField]
    public GameObject[] files;
    [SerializeField]
    public Transform[] locations;
    private Transform currentLocation;
    private GameObject currentFile;
    private int currentFileID = 0;
    private GameObject nextFile;
    private Transform nextLocation;

    public GameObject compass;

    private float waitTime = 1; //time it takes to rotate a file
    private float currentAngle = 0f;
    private bool isRotating = false;
    private float rotationDirection = 1f;


    // Use this for initialization
    void Start () {

        currentFile = Instantiate(files[currentFileID]);
        currentFile.transform.parent = this.transform;
        InteractionManager.InteractionSourcePressed += InteractionManager_SourcePressed;

    }
	
	// Update is called once per frame
	void Update () {
        
		if (isRotating)
        {
            float deltaAngle = rotationDirection * (Time.deltaTime / waitTime) * 360f;
            currentAngle += deltaAngle;
            currentFile.transform.Rotate(Vector3.forward, deltaAngle);
        } if (Mathf.Abs(currentAngle) >= 180f)
        {
            isRotating = false;
            currentAngle = 0f;
            Destroy(currentFile);
            currentFile = Instantiate(nextFile, this.transform);
            //currentFile.transform.parent = this.transform;
            //currentFile.transform.localPosition = Vector3.zero;
        }
	}

    void InteractionManager_SourcePressed(InteractionSourcePressedEventArgs state)
    {
        // Source was pressed. This will be after the source was detected and before it is released or lost
        // state has the current state of the source including id, position, kind, etc.

        if ((state.pressType == InteractionSourcePressType.Touchpad) && (state.state.source.handedness == InteractionSourceHandedness.Right) && !isRotating)
        {
            float posX = state.state.touchpadPosition.x;
            if (posX < 0)
            {
                currentFileID += 2;
                rotationDirection = 1f;
            } else
            {
                currentFileID++;
                rotationDirection = -1f;
            }
            nextFile = files[currentFileID % files.Count()];
            isRotating = true;
        }
    }
}
