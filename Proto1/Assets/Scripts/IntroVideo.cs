using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]
public class IntroVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while(!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();

        WaitForSeconds wait = new WaitForSeconds(1);
        while(videoPlayer.isPlaying)
        {
            yield return wait;
            break;
        }

        bool test = PlayerDataController.instance.LoadPlayerData();
        if (PlayerDataController.instance.LoadPlayerData())
        {

            LocalizationManager.instance.GetLanguageSettings();
            SceneManager.LoadScene("StartMenu");
        }
        else
        {
            SceneManager.LoadScene("LanguageSelection");
        }
    }
}
