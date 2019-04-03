using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{

    public PlayerDataController dataController;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        while (!LocalizationManager.instance.GetisReady())
        {
            // Wacht 1 frame
            yield return null;
        }
        PlayerDataController.instance.LoadPlayerData();
        //DataController dataController = DataController.instance;
        //dataController.LoadPlayerData();
        //dataController = gameObject.AddComponent<DataController>();


        SceneManager.LoadScene("StartMenu");
    }
}

