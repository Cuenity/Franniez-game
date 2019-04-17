using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerBallManager : MonoBehaviour
{
    GameState gameState;
    public Vector3 spawnpoint;
    public BlackHoleBall blackHoleBall;
    public NormalBall normalBall;
    public LightBall lightBall;



    // Start is called before the first frame update

    public void SetSpawnpoint(int i)
    {
        gameState = GameState.Instance;
        Vector3 playeradjustment = new Vector3(.5f, 0, 0);
        this.spawnpoint = gameState.gridManager.gridSquares[i] + playeradjustment;
    }

    public void InitTypeBall(string type)
    {
        switch (type)
        {
            case  "blackholeball":
               blackHoleBall= Instantiate(blackHoleBall);
                break;
            case "lightball":
                lightBall = Instantiate(lightBall);
                break;
            case "normalball":
                normalBall = Instantiate(normalBall);
                break;
            default:
                normalBall = Instantiate(normalBall);
                break;
        }
    }

}
