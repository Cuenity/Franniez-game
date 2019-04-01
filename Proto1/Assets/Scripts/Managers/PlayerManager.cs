using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Balletje balletje;
    public int collectedCoins;
    public bool collectedSticker;

    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        balletje = Instantiate(balletje);
        balletje.transform.position = new Vector3(1, 3, 0);
        CameraClass camera = GameObject.Find("Camera(Clone)").GetComponent<CameraClass>();
        camera.Target = balletje;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
