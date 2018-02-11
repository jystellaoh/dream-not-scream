using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Linq;

public class GlassPlacer : MonoBehaviour {

    public GameObject fogObject;
    private ParticleSystem fog;
    public List<GameObject> piecesInPlace = new List<GameObject>();
    private int currentlyInPlace;
    private int neededInPlace;

    public void Start()
    {
        currentlyInPlace = 0;
        neededInPlace = 6;
        fog = fogObject.GetComponent<ParticleSystem>();
        foreach (Renderer piece in GetComponentsInChildren<Renderer>())
        {
            piecesInPlace.Add(piece.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject piece in piecesInPlace)
        {
            
            if (other.gameObject.name.Equals(piece.name))
            {
                piece.gameObject.SetActive(true);
                Destroy(other.gameObject);
                fog.maxParticles -= 200; 
                currentlyInPlace++; 
                if (currentlyInPlace == neededInPlace)
                {
                    FadeManager.Instance.DoFade(5f, 0f, null, null);
                    GameStateManager.CheckState();
                    GameStateManager.dream3Completed = true;
                    Invoke("SwitchToMain", 5f);
                }
            }
        }
    }

    public void SwitchToMain()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("CameraParent");
        Debug.Log("num cameras " + cameras.Count());
        foreach (GameObject camera in cameras)
        {
            MotionControllerVisualizer vis = camera.GetComponentInChildren<MotionControllerVisualizer>();
            Debug.Log(camera.scene.buildIndex);
            if (camera.scene.buildIndex != 6)
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
        SceneManager.LoadScene(6);
    }
}
