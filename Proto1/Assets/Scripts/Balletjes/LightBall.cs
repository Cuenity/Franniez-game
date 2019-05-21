using Photon.Pun;
using UnityEngine;

public class LightBall : MonoBehaviourPun
{
    // Start is called before the first frame update
    private GameState gameState;
    public GameObject lightballEffect;

    private void Awake()
    {
        gameState = GameState.Instance;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<SphereCollider>().isTrigger = true;
    }

    void Start()
    {
        lightballEffect = Instantiate(lightballEffect);
        lightballEffect.transform.SetParent(this.transform);
        lightballEffect.transform.position = this.transform.position;
        this.GetComponent<Rigidbody>().maxAngularVelocity = 99;
    }

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
