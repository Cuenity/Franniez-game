using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Balletje balletje;
    public CameraManager cameraManager;
    // Start is called before the first frame update
    private void Awake()
    {
        //balletje = gameObject.AddComponent<Balletje>();

    }
    void Start()
    {
        SpawnBal();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnBal()
    {
        balletje = Instantiate(balletje);
        balletje.transform.position = new Vector3(1, 3, 1);
        cameraManager = gameObject.AddComponent<CameraManager>();
    }
}
