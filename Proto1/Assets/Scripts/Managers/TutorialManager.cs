using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialArrow;

    private GameObject arrow;

    private bool rollingFinished;
    public bool RollingFinished
    {
        get
        {
            return rollingFinished;
        }
        set
        {
            rollingFinished = value;
            if (rollingFinished)
            {
                SpawnTutorialArrow();
            }
            else
            {
                RemoveTutorialArrow();
            }
        }
    }

    public void TurnTutorialMaskOff()
    {
        GameState.Instance.UIManager.canvas.GetComponentInChildren<TutorialMask>(true).gameObject.SetActive(false);
    }

    public void StartTutorial()
    {

        GameState.Instance.UIManager.canvas.gameObject.transform.Find("StartButton").GetComponent<ButtonManager>().tutorialActive = true;

        StartCoroutine(SpawnTutorialMaskAfterSecond());
    }

    IEnumerator SpawnTutorialMaskAfterSecond()
    {
        yield return new WaitForSeconds(1);
        GameState.Instance.UIManager.canvas.GetComponentInChildren<TutorialMask>(true).gameObject.SetActive(true);
        GameState.Instance.UIManager.instantiatedInventoryButtons[0].gameObject.SetActive(false);
    }

    private void SpawnTutorialArrow()
    {
        if (arrow != null)
        {
            arrow = Instantiate(tutorialArrow, new Vector3(3.5f, -4, -4), new Quaternion(0, 0, 0, 0));
        }
    }

    private void RemoveTutorialArrow()
    {
        if (tutorialArrow != null)
        {
            Destroy(tutorialArrow);
        }
    }
}
