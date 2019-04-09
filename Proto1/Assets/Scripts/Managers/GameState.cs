using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public BuildingPhaseManager buildingPhaseManager;
    public RollingPhaseManager rollingPhaseManager;
    IOManager IOManager;
    InputManager InputManager;
    public UIManager UIManager;
    public LevelManager levelManager;
    public PlayerManager playerManager;
    public PlatformManager platformManager;
    public CollectableManager collectableManager;
    private bool buildingPhaseActive = true;
    private bool rollingPhaseActive = false;
    public PlayerCamera playerCamera;
    Scene currentScene;
    public GridManager gridManager;

    private static GameState instance;
    public static GameState Instance
    {
        get
        {
            return instance;
        }
    }

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
                UIManager.RemoveInventoryButtons();
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

        instance = this;

        buildingPhaseManager = gameObject.AddComponent<BuildingPhaseManager>();
        rollingPhaseManager = gameObject.AddComponent<RollingPhaseManager>();
        collectableManager = gameObject.AddComponent<CollectableManager>();
        IOManager = gameObject.AddComponent<IOManager>();
        InputManager = gameObject.AddComponent<InputManager>();
        UIManager = gameObject.AddComponent<UIManager>();
        levelManager = gameObject.AddComponent<LevelManager>();
        playerManager = gameObject.AddComponent<PlayerManager>();
        platformManager = gameObject.AddComponent<PlatformManager>();
        gridManager = gameObject.AddComponent<GridManager>();
        //playerCamera = Instantiate(playerCamera);

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