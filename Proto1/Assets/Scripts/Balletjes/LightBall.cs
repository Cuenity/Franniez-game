using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBall : MonoBehaviourPun
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
        this.GetComponent<Rigidbody>().maxAngularVelocity = 99;

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
        if (this.transform.position.y < gameState.gridManager.height * -1 || this.transform.position.x < 0 || this.transform.position.x > gameState.gridManager.width)
        {
            Handheld.Vibrate();
            gameState.levelManager.SetBuildingPhase();
        }
    }
}
