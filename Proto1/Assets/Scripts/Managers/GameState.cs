using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public BuildingPhaseManager buildingPhaseManager;
    public RollingPhaseManager rollingPhaseManager;
    IOManager IOManager;
    InputManager InputManager;
    UIManager UIManager;
    LevelManager levelManager;
    public PlayerManager playerManager;
    public PlatformManager platformManager;
    public CollectableManager collectableManager;
    private bool buildingPhaseActive = true;
    private bool rollingPhaseActive = false;
    public CameraClass cameraClass;
    Scene currentScene;

    //private CameraClass camera;

    public GameObject fixCamera;
    private Camera cameraReal;


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
        playerManager = gameObject.AddComponent<PlayerManager>();
        platformManager = gameObject.AddComponent<PlatformManager>();
        cameraClass = Instantiate(cameraClass);

        levelManager.InitScene();
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