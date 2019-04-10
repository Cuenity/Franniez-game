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

    public int PreviousLevel;

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

        buildingPhaseManager = Instantiate(buildingPhaseManager, instance.transform);
        buildingPhaseManager.transform.parent = this.transform;
        rollingPhaseManager = Instantiate(rollingPhaseManager, instance.transform);
        rollingPhaseManager.transform.parent = this.transform;
        collectableManager = Instantiate(collectableManager, instance.transform);
        collectableManager.transform.parent = this.transform;
        //IOManager = Instantiate(IOManager, instance.transform);
        //IOManager.transform.parent = this.transform;
        //InputManager = Instantiate(InputManager, instance.transform);
        //InputManager.transform.parent = this.transform;
        UIManager = Instantiate(UIManager, instance.transform);
        UIManager.transform.parent = this.transform;
        levelManager = Instantiate(levelManager, instance.transform);
        levelManager.transform.parent = this.transform;
        playerManager = Instantiate(playerManager, instance.transform);
        playerManager.transform.parent = this.transform;
        platformManager = Instantiate(platformManager, instance.transform);
        platformManager.transform.parent = this.transform;
        gridManager = Instantiate(gridManager, instance.transform);
        gridManager.transform.parent = this.transform;

        //PreviousScene = 0;

        //buildingPhaseManager = gameObject.AddComponent<BuildingPhaseManager>();
        //rollingPhaseManager = gameObject.AddComponent<RollingPhaseManager>();
        //collectableManager = gameObject.AddComponent<CollectableManager>();
        //IOManager = gameObject.AddComponent<IOManager>();
        //InputManager = gameObject.AddComponent<InputManager>();
        //UIManager = gameObject.AddComponent<UIManager>();
        //levelManager = gameObject.AddComponent<LevelManager>();
        //playerManager = gameObject.AddComponent<PlayerManager>();
        //platformManager = gameObject.AddComponent<PlatformManager>();
        //gridManager = gameObject.AddComponent<GridManager>();
        //playerCamera = Instantiate(playerCamera);

        //levelManager.InitScene();
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