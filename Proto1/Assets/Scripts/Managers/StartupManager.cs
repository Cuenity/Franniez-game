using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        while (!LocalizationManager.instance.GetisReady())
        {
            // Wacht 1 frame
            yield return null;
        }

        SceneManager.LoadScene("StartMenu");
    }
}

