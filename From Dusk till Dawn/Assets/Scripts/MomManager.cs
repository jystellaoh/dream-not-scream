using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MomManager : MonoBehaviour {

    [SerializeField]
    public AudioSource[] phrases;
    [SerializeField]
    public AudioSource winningAudio;
    [SerializeField]
    public AudioSource failAudio;

    [SerializeField]
    public GameObject[] toys;
    public Transform toySpawnPoint;

    public GameObject currentToy;


    private float timeSinceLastPhrase = 0f;
    private float timeBetweenPhrases = 5f;

    private int collectedToys = 0;
    private int neededToys = 1;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if ((Time.time - timeSinceLastPhrase >= timeBetweenPhrases))
        {
            BeAngry();
            SetCurrentToy();
            timeSinceLastPhrase = Time.time;
        }

    }

    void SetCurrentToy()
    {
        System.Random rndP = new System.Random();
        GameObject newToy = toys[(int)rndP.Next(0, toys.Count())];
        Destroy(currentToy);
        currentToy = Instantiate(newToy, toySpawnPoint);
        currentToy.transform.localPosition = Vector3.zero;
        currentToy.transform.localScale = Vector3.one * 0.1f;
    }

    void CheckCurrentToy(GameObject toy)
    {
        if (currentToy.name.Contains(toy.name))
        {

            Destroy(toy);
            winningAudio.Play();
            collectedToys++;
            if (collectedToys == neededToys)
            {
                GameStateManager.dream1Completed = true;
                GameStateManager.CheckState();
                FadeManager.Instance.DoFade(5f, 0f, null, null);
                Invoke("GoBackToMain", 5);
            }
        }
        else
        {
            BeAngry();
            SetCurrentToy();
        }
    }

    void BeAngry()
    {
        System.Random rndP = new System.Random();
        AudioSource randomPhrase = phrases[(int)rndP.Next(0, phrases.Count())];
        randomPhrase.Play();
    }

    private void GoBackToMain()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("CameraParent");
        Debug.Log("num cameras " + cameras.Count());
        foreach (GameObject camera in cameras)
        {
            MotionControllerVisualizer vis = camera.GetComponentInChildren<MotionControllerVisualizer>();
            Debug.Log(camera.scene.buildIndex);
            if (camera.scene.buildIndex != 4)
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
        SceneManager.LoadScene(4);
    }
}
