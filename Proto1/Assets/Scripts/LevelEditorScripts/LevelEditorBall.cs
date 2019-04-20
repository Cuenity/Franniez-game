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

        List<int> fillsGridSpot = new List<int>();
        fillsGridSpot.Add(i);
        GameState.Instance.gridManager.AddFilledGridSpots(fillsGridSpot, SizeType.oneByOne);
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
        //gameState = GameState.Instance;
        //Camera actualcamera = gameState.GetComponent<Camera>();
        //PlayerCamera camera = gameState.playerCamera;
        this.activePlayer.transform.position = spawnpoint;
        this.activePlayer.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        this.activePlayer.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //this.GetComponent<Rigidbody>().rotation = new Quaternion(0, 0, 0, 0);
        //camera.transform.position = new Vector3(gameState.playerBallManager.spawnpoint.x + camera.TargetMovementOffset.x, gameState.playerBallManager.spawnpoint.y + camera.TargetMovementOffset.y, gameState.playerBallManager.spawnpoint.z + camera.TargetMovementOffset.z);
        //camera.transform.LookAt(camera.Target.transform.position);
        //camera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().Sleep();
    }

    public void RespawnBal()
    {
        StartCoroutine(Respawnballinternal());
    }

    public void Roll()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().useGravity = true;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name.Contains("Trampoline"))
    //    {
    //        Vector3 velocity = this.GetComponent<Rigidbody>().velocity;
    //        float velocityY;
    //        float velocityx;
    //        if (velocity.y > 0)
    //        {
    //            velocityY = velocity.y;
    //        }
    //        else
    //        {
    //            velocityY = velocity.y * -1;
    //        }
    //        if ((velocityY < 4 && velocityY > 0) || velocityY == 0)
    //        {
    //            if (this.name.Contains("Light"))
    //            {
    //                velocityY = 2;
    //            }
    //            else
    //            {
    //                velocityY = 4;
    //            }
    //        }
    //        else if (velocityY > -4 && velocityY < 0)
    //        {
    //            if (this.name.Contains("Light"))
    //            {
    //                velocityY = -2;
    //            }
    //            else
    //            {
    //                velocityY = -4;
    //            }
    //        }

    //        if (velocity.x < 1 && velocity.x > 0)
    //        {
    //            if (this.name.Contains("Light"))
    //            {
    //                velocityx = 1;
    //            }
    //            else
    //            {
    //                velocityx = 2;
    //            }
    //        }
    //        else if ((velocity.x > -1 && velocity.x < 0) || velocity.x == 0)
    //        {
    //            if (this.name.Contains("Light"))
    //            {
    //                velocityx = -1;
    //            }
    //            else
    //            {
    //                velocityx = -2;
    //            }
    //        }
    //        else
    //        {
    //            velocityx = velocity.x;
    //        }

    //        this.GetComponent<Rigidbody>().AddForce(velocityx, velocityY * 7f, 0, ForceMode.Impulse);
    //    }
    //}
    //Deze krijg ik niet triggered dus de oplossing ontriggerenter is nodig
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        Vector3 velocity = this.GetComponent<Rigidbody>().velocity;
        float velocityY;
        float velocityx;
        if (velocity.y > 0)
        {
            velocityY = velocity.y;
        }
        else
        {
            velocityY = velocity.y * -1;
        }
        if ((velocityY < 4 && velocityY > 0) || velocityY == 0)
        {
            if (this.name.Contains("Light"))
            {
                velocityY = 2;
            }
            else
            {
                velocityY = 4;
            }
        }
        else if (velocityY > -4 && velocityY < 0)
        {
            if (this.name.Contains("Light"))
            {
                velocityY = -2;
            }
            else
            {
                velocityY = -4;
            }
        }

        if (velocity.x < 1 && velocity.x > 0)
        {
            if (this.name.Contains("Light"))
            {
                velocityx = 1;
            }
            else
            {
                velocityx = 2;
            }
        }
        else if ((velocity.x > -1 && velocity.x < 0) || velocity.x == 0)
        {
            if (this.name.Contains("Light"))
            {
                velocityx = -1;
            }
            else
            {
                velocityx = -2;
            }
        }
        else
        {
            velocityx = velocity.x;
        }

        this.GetComponent<Rigidbody>().AddForce(velocityx, velocityY * 7f, 0, ForceMode.Impulse);
    }

}
