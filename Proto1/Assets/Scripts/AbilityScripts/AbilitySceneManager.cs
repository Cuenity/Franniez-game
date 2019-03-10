using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AbilitySceneManager : MonoBehaviour {

    //classes om te instantiaten
    
    public PlayerBall playerBallClass;

    //text??
    public Text text;

    //usable variables voor update
    public Rigidbody playerRB;
    public Camera playerCamera;
    // Use this for initialization
    void Start () {
        Assert.IsNotNull(playerBallClass);
        
        

        playerRB = playerBallClass.GetComponent<Rigidbody>();
        playerRB.useGravity = false;
        //ga in scene slepen dus instantiaten niet nodig
        //playerCamera = Instantiate(playerCamera,new Vector3(0,1,-13.43f),new Quaternion(0,0,0,0));
        //PlayerBall playerBall = Instantiate(playerBallClass, new Vector3(-14.75f, 7.6f, 0), new Quaternion(0, 0, 0, 0));

    }
    

    //Dit dan 


    // Update is called once per frame
    void Update () {
        //start game
        text.text = playerBallClass.canJump.ToString() ;
        if (Input.GetKeyDown("s"))
        {
            playerRB.useGravity = true;
        }
        //reset game
        if (Input.GetKeyDown("r"))
        {
            PlayerBall playerBall = Instantiate(playerBallClass, new Vector3(-14.75f, 7.6f, 0), new Quaternion(0, 0, 0, 0));
            playerRB = playerBall.GetComponent<Rigidbody>();
        }
        //jump
        if (Input.GetKeyDown("q"))
        {
            
            playerBallClass.Jump();
        }
        if (Input.GetKeyDown("w"))
        {
            playerBallClass.StopMoving();
        }
        if (Input.GetKeyDown("e"))
        {

        }
    }
    //geen collision in de scenemanager lol
    private void OnCollisionEnter(Collision collision)
    {
        //platform interactie
        Vector3 goHardOrGoHome = new Vector3(0, 50, 0);

        //sla platform waarmee aanraking is op
        

        if (collision.collider.CompareTag("Bounce"))
        {
            Debug.Log("bouncersboncers");
            playerRB.AddForce(goHardOrGoHome, ForceMode.Impulse);
        }

        //voor obstakels
        if (collision.collider.CompareTag("Kill"))
        {
            text.text = "JE HEBT VERLOREN";
        }
        if (collision.collider.CompareTag("Finish"))
        {
            text.text = "JE HEBT GEWONNEN";
        }
    }


}
