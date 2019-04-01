using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    BuildingPhaseManager buildingPhaseManager;
    RollingPhaseManager rollingPhaseManager;
    IOManager IOManager;
    InputManager InputManager;
    UIManager UIManager;
    LevelManager levelManager;
    CollectableManager collectableManager;
    private bool buildingPhaseActive;
    private bool rollingPhaseActive;
    public CameraClass cameraClass;
    Scene currentScene;


    public bool BuildingPhaseActive
    {
        get
        {
            return buildingPhaseActive;
        }
        set
        {
            buildingPhaseActive = value;
            if (buildingPhaseActive)
            {
                buildingPhaseManager.Init();
            }
            else
            {

            }
        }
    }

    public bool RollingPhaseActive
    {
        get
        {
            return rollingPhaseActive;
        }
        set
        {
            rollingPhaseActive = value;
            if (rollingPhaseActive)
            {
                rollingPhaseManager.Init();
            }
            else
            {

            }
        }
    }


    void Awake()
    {
        DontDestroyOnLoad(this);

        buildingPhaseManager = gameObject.AddComponent<BuildingPhaseManager>();
        rollingPhaseManager = gameObject.AddComponent<RollingPhaseManager>();
        collectableManager = gameObject.AddComponent<CollectableManager>();

        IOManager = gameObject.AddComponent<IOManager>();
        InputManager = gameObject.AddComponent<InputManager>();
        UIManager = gameObject.AddComponent<UIManager>();
        levelManager = gameObject.AddComponent<LevelManager>();
        cameraClass = Instantiate(cameraClass);

        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "TestLevelCoen")
        {
            List<Vector3> coinPositions = new List<Vector3>();
            coinPositions.Add(new Vector3(0, 1.5f, 0));
            coinPositions.Add(new Vector3(3, 2, 0));
            coinPositions.Add(new Vector3(6, 2, 0));

            Vector3 stickerPosition = new Vector3(0, 0, 3);

            collectableManager.InitCollectables(coinPositions, stickerPosition);
        }
    }



    // Start is called before the first frame update
    void Start()
    {


        //PlayerManager.spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

}