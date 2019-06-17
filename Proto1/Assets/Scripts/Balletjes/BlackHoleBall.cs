using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleBall : MonoBehaviourPun
{
    GameState gameState = GameState.Instance;
    private Quaternion effectRotation=new Quaternion(0,0,0,0);
    public GameObject BHEffect;
    private void Awake()
    {
        gameState = GameState.Instance;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<SphereCollider>().isTrigger = true; // verander later ofzo
    }
    void Start()
    {
        this.GetComponent<Rigidbody>().maxAngularVelocity = 99;
        BHEffect = Instantiate(BHEffect);
        BHEffect.gameObject.transform.SetParent(this.gameObject.transform);
        BHEffect.transform.position = this.transform.position;
        BHEffect.GetComponent<Rigidbody>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(BHEffect.activeSelf ==true)
        {
            BHEffect.transform.position = this.transform.position;
        }
        else
        {
            BHEffect.SetActive(true);
            BHEffect.transform.position = this.transform.position;
        }
    }
    private void FixedUpdate()
    {
        if (this.transform.position.y < gameState.gridManager.height * -1 || this.transform.position.x < 0 || this.transform.position.x > gameState.gridManager.width)
        {
            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                Handheld.Vibrate();
            }
            gameState.levelManager.SetBuildingPhase();
        }
    }



}
