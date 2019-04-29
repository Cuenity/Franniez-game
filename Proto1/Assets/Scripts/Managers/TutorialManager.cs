using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialArrow;
    public bool RollingFinished
    {
        get { return RollingFinished; }
        set
        {
            RollingFinished = value;
            if (RollingFinished)
            {
                SpawnTutorialArrow();
            }
            else
            {
                RemoveTutorialArrow();
            }
        }
    }

    public void StartTutorial()
    {

        StartCoroutine(SpawnTutorialMaskAfterSecond());
    }

    IEnumerator SpawnTutorialMaskAfterSecond()
    {
        yield return new WaitForSeconds(1);
        GameState.Instance.UIManager.canvas.GetComponentInChildren<TutorialMask>(true).gameObject.SetActive(true);
    }

    private void SpawnTutorialArrow()
    {
        if (tutorialArrow != null)
        {
            Instantiate(tutorialArrow, new Vector3(3.5f, -4, -4), new Quaternion(0, 0, 45, 0));
        }
        //}
    }

    private void RemoveTutorialArrow()
    {
        if (tutorialArrow != null)
        {
            Destroy(tutorialArrow);
        }
    }
}
