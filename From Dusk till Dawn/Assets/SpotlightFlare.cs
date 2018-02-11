using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightFlare : MonoBehaviour {

    Vector3 startPosition;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
     }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += new Vector3(0, 100f, 0);
        if (transform.position.y > 10000f)
        {
            transform.position = startPosition;
        }
		
	}
}
