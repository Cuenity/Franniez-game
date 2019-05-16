using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBall : MonoBehaviourPun
{
    // Start is called before the first frame update
    GameState gameState = GameState.Instance;
    public GameObject lightballEffect;
    private void Awake()
    {
        gameState = GameState.Instance;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<SphereCollider>().isTrigger = true; // verander later ofzo
    }
    void Start()
    {
        lightballEffect = Instantiate(lightballEffect);
        lightballEffect.transform.SetParent(this.transform);
        lightballEffect.transform.position = this.transform.position;
        this.GetComponent<Rigidbody>().maxAngularVelocity = 99;

    }

    // Update is called once per frame
    void Update()
    {
        lightballEffect.transform.position = this.transform.position;
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
