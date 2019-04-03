using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{

    public DataController dataController;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        while (!LocalizationManager.instance.GetisReady())
        {
            // Wacht 1 frame
            yield return null;
        }

        dataController = gameObject.AddComponent<DataController>();


        SceneManager.LoadScene("StartMenu");
    }
}

