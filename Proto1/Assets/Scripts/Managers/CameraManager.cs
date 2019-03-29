using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CameraClass cameraclass;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        SpawnCamera();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnCamera()
    {
        //camera = Instantiate(camera);
        cameraclass = gameObject.AddComponent<CameraClass>(); 

    }


}

