﻿using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialArrow;

    private GameObject arrow;
    private GameObject arrow2;

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
                SpawnTutorialArrows();
            }
            else
            {
                RemoveTutorialArrows();
            }
        }
    }

    public void TurnTutorialMaskOff()
    {
        GameState.Instance.UIManager.canvas.GetComponentInChildren<TutorialMask>(true).gameObject.SetActive(false);
    }

    public void StartTutorial()
    {

        StartCoroutine(SetTutorialActiveAsSoonAsPossible());


        StartCoroutine(SpawnTutorialMaskAfterSecond());
    }

    IEnumerator SetTutorialActiveAsSoonAsPossible()
    {
        bool done = false;
        while (!done)
        {
            if (GameState.Instance.UIManager.canvas != null)
            {
                GameState.Instance.UIManager.canvas.gameObject.transform.Find("StartButton").GetComponent<ButtonManager>().tutorialActive = true;

                if (GameState.Instance.UIManager.instantiatedInventoryButtons.Length > 0)
                {
                    if (GameState.Instance.UIManager.instantiatedInventoryButtons[0] != null)
                    {
                        GameState.Instance.UIManager.instantiatedInventoryButtons[0].gameObject.SetActive(false);
                        done = true;
                    }
                }
            }
            yield return null;
        }
    }

    IEnumerator SpawnTutorialMaskAfterSecond()
    {
        yield return new WaitForSeconds(0.2f);
        GameState.Instance.UIManager.canvas.GetComponentInChildren<TutorialMask>(true).gameObject.SetActive(true);
    }

    private void SpawnTutorialArrows()
    {
        if (arrow == null)
        {
            arrow = Instantiate(tutorialArrow, new Vector3(3.5f, -4.3f, -4), Quaternion.Euler(0, 0, 45));
        }
        if (arrow2 == null)
        {
            arrow2 = Instantiate(tutorialArrow, new Vector3(3.5f, -3.3f, -4), Quaternion.Euler(0, 0, 20));
        }
    }

    public void RemoveTutorialArrows()
    {
        if (arrow != null)
        {
            Destroy(arrow);
        }
        if (arrow2 != null)
        {
            Destroy(arrow2);
        }
    }
}