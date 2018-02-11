using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingAnimation : MonoBehaviour
{

    private float jumpHeight = 0.01f;
    private float timer;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += (Time.deltaTime) * 7;
        transform.position += new Vector3(0, Mathf.Sin(timer) * jumpHeight, 0);
    }
}
