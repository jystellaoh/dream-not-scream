using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAnimation : MonoBehaviour
{

    private float jumpHeight = 0.01f;
    private float scaleChange = 0.01f;
    private float timer;


    // Update is called once per frame
    void FixedUpdate()
    {
        {
            timer += (Time.deltaTime) * 10;
            transform.position += new Vector3(0, Mathf.Sin(timer) * jumpHeight, 0);
            transform.localScale += new Vector3(Mathf.Sin(timer) * scaleChange, Mathf.Sin(timer) * scaleChange, Mathf.Sin(timer) * scaleChange);
        }
    }
}
