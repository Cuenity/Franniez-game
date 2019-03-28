using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //classes om te instantiaten
    public PlayerMouse playerMouseClass;
    
    public PlayerBall playerBallClass;

    //text??
    public Text text;

    //usable variables voor update
    public Rigidbody playerRB;
    public Camera playerCamera;

    

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(playerMouseClass);
        Assert.IsNotNull(playerBallClass);

        PlayerMouse playerMouse = Instantiate(playerMouseClass);
        PlayerBall playerBall = Instantiate(playerBallClass,new Vector3(-14.75f,7.6f,0),new Quaternion(0,0,0,0));

        playerRB = playerBall.GetComponent<Rigidbody>();

        //playerCamera = Instantiate(playerCameraClass,new Vector3(0,1,-13.43f),new Quaternion(0,0,0,0));

    }
    

    //Dit dan 


    // Update is called once per frame
    void Update () {
        //start game
        if (Input.GetKeyDown("q"))
        {
            playerRB.isKinematic = false;
        }
        //reset game
        if (Input.GetKeyDown("/"))
        {
            PlayerBall playerBall = Instantiate(playerBallClass, new Vector3(-14.75f, 7.6f, 0), new Quaternion(0, 0, 0, 0));
            playerRB = playerBall.GetComponent<Rigidbody>();
        }
        //FreeformScene
        if (Input.GetKeyDown("["))
        {
            SceneManager.LoadScene(sceneName: "ProtoScene 1");
            text.text = "FREEFORM PLACEMENT $$$$$";
        }

        //ObstakelScene
        if (Input.GetKeyDown("p"))
        {
            SceneManager.LoadScene(sceneName:"Obstakels");
            text.text = "Rood=Dood Groen is te doen";
        }
    }

    
}
