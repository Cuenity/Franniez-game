using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PitchSceneManager : MonoBehaviour {

    //classes om te instantiaten
    
    public PlayerBall playerBallClass;

    //text??
    public Text text;

    //usable variables voor update
    public Rigidbody playerRB;
    public Camera playerCamera;
    // Use this for initialization
    private PlayerBall playerBall;

    void Start () {
        Assert.IsNotNull(playerBallClass);
        
        

        
        //ga in scene slepen dus instantiaten niet nodig
        //playerCamera = Instantiate(playerCamera,new Vector3(0,1,-13.43f),new Quaternion(0,0,0,0));
        //toch wel handig dit te doen als je player balls wilt killen
        playerBall = Instantiate(playerBallClass, new Vector3(-24.2f, 17.5f, 0), new Quaternion(0, 0, 0, 0));
        playerRB = playerBall.GetComponent<Rigidbody>();
        playerRB.useGravity = false;

    }
    

    //Dit dan 


    // Update is called once per frame
    void Update () {
        //start game
        UITextFormattter();
        if (Input.GetKeyDown("s"))
        {
            playerRB.useGravity = true;
        }
        //reset game
        if (Input.GetKeyDown("r"))
        {
            playerBall = Instantiate(playerBallClass, new Vector3(-24.2f, 17.5f, 0), new Quaternion(0, 0, 0, 0));
            playerRB = playerBall.GetComponent<Rigidbody>();
        }
        //jump
        if (Input.GetKeyDown("q"))
        {
            
            playerBall.Jump();
        }
        if (Input.GetKeyDown("w"))
        {
            playerBall.StopMoving();
        }
        if (Input.GetKeyDown("e"))
        {
            playerBall.IncreaseSpeed();
        }
    }
    public void UITextFormattter()
    {
        text.text = $"Press S to start \n" +
                    $"Jump Ready:{playerBall.canJump} \n" +
                    $"Boost Ready:{playerBall.canBoost} \n" +
                    $"Player is Alive:{playerBall.isAlive}(press r to restart) \n" +
                    $"PlayerPoints:{playerBall.points} \n" +
                    $"Player has Won{playerBall.playerHitGoal}(hit the black block) \n";


    }
   


}
