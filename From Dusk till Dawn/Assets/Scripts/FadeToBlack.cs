using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToBlack : MonoBehaviour {

    // Use this for initialization
    void Start() {
        FadeManager.Instance.DoFade(24f, 0f, null, null);
        Invoke("End", 24f);

    }

    // Update is called once per frame
    void End()
    {
        SceneManager.LoadScene(8);
    }
}
