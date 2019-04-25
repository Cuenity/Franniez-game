using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    GameState gameState;
    int index = 0;
    bool used = false;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!used)
        {
            GameObject bal = gameState.playerBallManager.activePlayer;
            Rigidbody body = bal.GetComponent<Rigidbody>();
            Vector3 oldVelocity = body.velocity;
            body.velocity = new Vector3(0, 0, 0);
            Quaternion oldRotation = body.rotation;
            body.freezeRotation = true;
            bal.transform.position = this.transform.position + new Vector3(0, 1, 0);
            StartCoroutine(Move(oldVelocity, oldRotation, body, bal));
        }

    }
    public void GoToEnd()
    {



    }

    private IEnumerator Move(Vector3 velocity, Quaternion rotation, Rigidbody body, GameObject ball)
    {
        Vector3 diffrence = (this.endPoint - this.startPoint) / 100;
        while (index <= 100)
        {
            this.transform.position = this.transform.position + diffrence;
            ball.transform.position = this.transform.position + new Vector3(0, .75f, 0);
            index++;
            yield return new WaitForEndOfFrame();
        }
        body.velocity = velocity;
        //body.rotation = rotation;
        body.freezeRotation = false;
        index = 0;
        this.used = true;
    }

    public void SetStartAndEndPoints(int start, int end)
    {
        gameState = GameState.Instance;
        this.startPoint = gameState.gridManager.gridSquares[start] + new Vector3(1f, 0, 0);
        this.endPoint = gameState.gridManager.gridSquares[end] + new Vector3(1f, 0, 0);
    }

    public void ResetPlatform()
    {
        this.transform.position = this.startPoint;
        this.used = false;
    }

}

