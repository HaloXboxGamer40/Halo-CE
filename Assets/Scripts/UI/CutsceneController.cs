using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    // Reference to the VideoPlayer component
    public VideoPlayer videoPlayer;

    // Reference to the GameObject cutsceneCam
    public GameObject cutsceneCam;

    // Reference to the GameObject map
    public GameObject map;

    public GameObject player;

    public GameObject skipText;

    public GameObject bg;
    public GameObject bgimg;
    public GameObject audio;

    public bool skipCutscene;

    void Start()
    {
        // Subscribe to the VideoPlayer's loopPointReached event to detect when the video is finished
        videoPlayer.loopPointReached += OnVideoEnd;

        if (skipCutscene)
            StartCoroutine(skipTextCode());

        skipText.SetActive(false);
        cutsceneCam.SetActive(true);
        player.SetActive(false);
    }

    void Update()
    {
        // Check if the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && skipCutscene)
        {
            EndCutscene();
        }
    }

    // Event handler for when the video ends
    void OnVideoEnd(VideoPlayer vp)
    {
        EndCutscene();
    }

    // Method to handle ending the cutscene
    void EndCutscene()
    {
        // Stop the video
        videoPlayer.Stop();

        // Hide the cutsceneCam GameObject
        cutsceneCam.SetActive(false);

        skipText.SetActive(false);

        // Show the map GameObject
        if (skipCutscene) {
            map.SetActive(true);

            player.SetActive(true);
        } else {
            bg.SetActive(true);

            bgimg.SetActive(true);
            audio.SetActive(true);
        }
        // Destroy the VideoPlayer GameObject (if it is separate)
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    IEnumerator skipTextCode()
    {
        yield return new WaitForSeconds(4f);
        Destroy(skipText);
    }
}