using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBall : MonoBehaviour
{
    // Start is called before the first frame update
    GameState gameState = GameState.Instance;
    private void Awake()
    {
        gameState = GameState.Instance;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<SphereCollider>().isTrigger = true; // verander later ofzo
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.RollingPhaseActive)
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<SphereCollider>().isTrigger = false;
        }
        else
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<SphereCollider>().isTrigger = true;
        }
    }
        private void FixedUpdate()
    {
        if (this.transform.position.y < gameState.gridManager.heigth * -1 || this.transform.position.x < 0 || this.transform.position.x > gameState.gridManager.width + 1)
        {
            Handheld.Vibrate();
            gameState.levelManager.SetBuildingPhase();
        }
    }

    private IEnumerator respawnballinternal()
    {
        yield return new WaitForEndOfFrame();
        gameState = GameState.Instance;
        Camera actualcamera = gameState.GetComponent<Camera>();
        PlayerCamera camera = gameState.playerCamera;
        this.transform.position = gameState.playerBallManager.spawnpoint;
        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //this.GetComponent<Rigidbody>().rotation = new Quaternion(0, 0, 0, 0);
        camera.transform.position = new Vector3(gameState.playerBallManager.spawnpoint.x + camera.TargetMovementOffset.x, gameState.playerBallManager.spawnpoint.y + camera.TargetMovementOffset.y, gameState.playerBallManager.spawnpoint.z + camera.TargetMovementOffset.z);
        camera.transform.LookAt(camera.Target.transform.position);
        camera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().Sleep();
    }

    public void respawnBal()
    {
        StartCoroutine(respawnballinternal());
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if(other.name.Contains("Star"))
        {
            //Debug.Log("JE MOEDER");
        }
    }

}
