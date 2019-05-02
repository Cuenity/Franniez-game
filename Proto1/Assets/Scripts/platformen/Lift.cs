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
    public IEnumerator coroutineLift;
    public GameObject invisLift;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Instance;
        invisLift = Instantiate(invisLift);
        invisLift.transform.position = this.endPoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // check of de lift al gebruikt is, freeze lift, en start coroutine
        if (!used)
        {
            GameObject bal = gameState.playerBallManager.activePlayer;
            Rigidbody body = bal.GetComponent<Rigidbody>();
            Vector3 oldVelocity = body.velocity;
            body.velocity = new Vector3(0, 0, 0);
            body.freezeRotation = true;
            bal.transform.position = this.transform.position + new Vector3(0, 1, 0);
            coroutineLift = Move(oldVelocity, body, bal);
            StartCoroutine(coroutineLift);
        }
    }


    private IEnumerator Move(Vector3 velocity, Rigidbody body, GameObject ball)
    {
        this.used = true;
        // bereken de verandering in 100 stapjes
        Vector3 diffrence = (this.endPoint - this.startPoint) / 100;
        body.useGravity = false;
        body.isKinematic = true;
        // voer de transform uit in 100 stapjes zodat het smooth lijkt. en neem het balletje mee
        while (index <= 100)
        {
            this.transform.position = this.transform.position + diffrence;
            ball.transform.position = this.transform.position + new Vector3(0, .8f, 0);
            index++;
            yield return new WaitForEndOfFrame();
        }

        // geef hem zijn oude snelheid weermee laat de rotatie weer verdergaan
        body.velocity = velocity;
        body.freezeRotation = false;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        body.useGravity = true;
        index = 0;
        
    }

    public void SetStartAndEndPoints(int start, int end)
    {
        gameState = GameState.Instance;
        this.startPoint = gameState.gridManager.gridSquares[start] + new Vector3(1f, 0, 0);
        this.endPoint = gameState.gridManager.gridSquares[end] + new Vector3(1f, 0, 0);
    }

    // reset het platform naar zijn startpositie en zorg dat de coroutine gestopt word.
    public void ResetPlatform()
    {
        //dit is broken en wordt vaker aangeroepen dan nodig is?
        GameObject ball = GameState.Instance.playerBallManager.activePlayer;
        try
        {
            StopCoroutine(coroutineLift);
            index = 0;
            ball.GetComponent<Rigidbody>().useGravity = true;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            if( ball.GetComponent<Rigidbody>().freezeRotation== true)
            {
                ball.GetComponent<Rigidbody>().freezeRotation = false;
            }
        }
        catch
        {

        }
        this.transform.position = this.startPoint;
        this.used = false;
    }

}

