using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update

    public static MainMenuMusic instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (this != null)
        {
            Destroy(gameObject);
        }
    }

    // Dit moet veranderd worden naar scenemanager
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "1" || SceneManager.GetActiveScene().name == "VictoryScreen")
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
        else
        {
            if(!gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
}
