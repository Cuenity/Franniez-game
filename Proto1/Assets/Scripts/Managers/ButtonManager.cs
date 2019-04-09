using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameState gameState;

    public delegate void ClickAction();
    public static event ClickAction ChangeEnvironment;

    void Start()
    {
        gameState = GameState.Instance;
    }

    void Update()
    {

    }

    public void StartButton()
    {
        if (gameState.BuildingPhaseActive == true)
        {
            SetRollingPhase();
        }
        else if (gameState.RollingPhaseActive == true)
        {
            SetBuildingPhase();
        }
        ChangeEnvironment();
    }
    public void SetRollingPhase()
    {
        gameState.BuildingPhaseActive = false;
        gameState.RollingPhaseActive = true;
        Time.timeScale = 1;
    }
    public void SetBuildingPhase()
    {
        gameState.playerManager.player.respawnBal();
        gameState.levelManager.RespawnCollectables();
        Time.timeScale = 0;
    }

    public void InventoryButton(GameObject platformSquare)
    {
        //Instantiate(platformSquare);
        //var pos = Input.mousePosition;

    }

    public void TestLang(string pathFile)
    {
        LocalizationManager.instance.LoadLocalizedText(pathFile);


        Player player = new Player{name = ""};
        PlayerDataController.instance.player = player;
        PlayerDataController.instance.Save();
        LocalizationManager.instance.ReturnLanguage(pathFile);
        SceneManager.LoadScene("StartMenu");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void NextLevel()
    {
        int levelNumber = 2;
        //int levelNumber = GameState.Instance.PreviousLevel + 1;

        SceneManager.LoadScene(levelNumber.ToString());
    }

    public void RestartScene()
    {
        int levelNumber = 1;
        //int levelNumber = GameState.Instance.PreviousLevel;

        SceneManager.LoadScene(levelNumber.ToString());
    }
}
