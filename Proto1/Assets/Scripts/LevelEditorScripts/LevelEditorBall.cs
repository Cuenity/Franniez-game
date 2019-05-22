using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class LevelEditorBall : MonoBehaviour
{
    public Vector3 spawnpoint;
    public BlackHoleBall blackHoleBall;
    public NormalBall normalBall;
    public LightBall lightBall;
    public GameObject activePlayer;



    // Start is called before the first frame update

    public void SetSpawnpoint(int i)
    {
        Vector3 playeradjustment = new Vector3(.5f, 0, 0);
        spawnpoint = LevelEditorState.Instance.gridManager.gridSquares[i] + playeradjustment;
        GameState.Instance.gridManager.AddFilledGridSpots(new List<int> { i }, SizeType.oneByOne);
    }

    public void InitTypeBall(Bal type)
    {
        switch (type)
        {
            case Bal.BlackHole:
                activePlayer = Instantiate(blackHoleBall).gameObject;
                //gameState.playerBallManager.activePlayer = activePlayer;
                //gameState.playerCamera.Target = activePlayer;
                break;
            case Bal.Light:
                activePlayer = Instantiate(lightBall).gameObject;
                //gameState.playerBallManager.activePlayer = activePlayer;
                //gameState.playerCamera.Target = activePlayer;
                break;
            case Bal.Normal:
                activePlayer = Instantiate(normalBall).gameObject;
                //gameState.playerBallManager.activePlayer = activePlayer;
                //gameState.playerCamera.Target = activePlayer;
                break;
            default:
                activePlayer = Instantiate(normalBall).gameObject;
                //gameState.playerBallManager.activePlayer = activePlayer;
                //gameState.playerCamera.Target = activePlayer;
                break;
        }
    }
    private IEnumerator Respawnballinternal()
    {
        yield return new WaitForEndOfFrame();

        this.activePlayer.transform.position = spawnpoint;
        this.activePlayer.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        this.activePlayer.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        this.GetComponent<Rigidbody>().Sleep();
    }

    public void RespawnBal()
    {
        StartCoroutine(Respawnballinternal());
    }

    public void Roll()
    {
        Instantiate(this, LevelEditorState.Instance.gridManager.gridSquares[LevelEditorState.Instance.SpawnBallPosition]+new Vector3(0.5f,0,0), new Quaternion(0, 0, 0, 0));
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().maxAngularVelocity = 99;
        this.GetComponent<Rigidbody>().mass = 3;
    }
}
