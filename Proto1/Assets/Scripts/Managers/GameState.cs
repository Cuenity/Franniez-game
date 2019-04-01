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