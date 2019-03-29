using UnityEngine;

public class GameState : MonoBehaviour
{
    BuildingPhaseManager buildingPhaseManager;
    RollingPhaseManager rollingPhaseManager;
    IOManager IOManager;
    InputManager InputManager;
    UIManager UIManager;
    LevelManager levelManager;

    public bool BuildingPhaseActive
    {
        get
        {
            return BuildingPhaseActive;
        }
        set
        {
            BuildingPhaseActive = value;
            if (BuildingPhaseActive)
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
            return RollingPhaseActive;
        }
        set
        {
            RollingPhaseActive = value;
            if (RollingPhaseActive)
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

        IOManager = gameObject.AddComponent<IOManager>();
        InputManager = gameObject.AddComponent<InputManager>();
        UIManager = gameObject.AddComponent<UIManager>();
        levelManager = gameObject.AddComponent<LevelManager>();
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
