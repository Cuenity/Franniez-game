using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMouse : MonoBehaviour {

    public Platform[] platforms;
    private Vector3 mousepos;

    public previewPlatform[] platformsPreview;
    private previewPlatform platformPreview;
    private Vector3 rotation;

    private int platformIndex = 0;
    
    
    

    // Use this for initialization
    void Start () {
        mousepos = Input.mousePosition;
        mousepos.z = 13;

        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        rotation = new Vector3(0, 0, 0);

        platformPreview =  Instantiate(platformsPreview[0], mousepos, Quaternion.identity);
        
        
    }
	
	// Update is called once per frame
	void Update () {

        mousepos = Input.mousePosition;
        mousepos.z = 22;

        mousepos = Camera.main.ScreenToWorldPoint(mousepos);

        if (Input.GetKeyDown("r"))
        {
            rotation.z += 22.5f;
        }

        if (Input.GetKeyDown("1"))
        {
            platformIndex = 0;
            Destroy(platformPreview.gameObject);
            platformPreview = Instantiate(platformsPreview[platformIndex], mousepos, Quaternion.identity);
        }

        if (Input.GetKeyDown("2"))
        {
            platformIndex = 1;
            Destroy(platformPreview.gameObject);
            platformPreview = Instantiate(platformsPreview[platformIndex], mousepos, Quaternion.identity);
        }

        if (Input.GetKeyDown("3"))
        {
            platformIndex = 2;
            Destroy(platformPreview.gameObject);
            platformPreview = Instantiate(platformsPreview[platformIndex], mousepos, Quaternion.identity);
        }

        platformPreview.transform.position = mousepos;
        platformPreview.transform.eulerAngles = rotation;

		if (Input.GetMouseButtonUp(0))
        {
            InstructieBehaviour.PlaceBlock();
            mousepos = Input.mousePosition;
            mousepos.z = 22;

            platforms[platformIndex].transform.position = mousepos;
            platforms[platformIndex].transform.eulerAngles = rotation;

            mousepos = Camera.main.ScreenToWorldPoint(mousepos);
            Instantiate(platforms[platformIndex], mousepos, Quaternion.Euler(rotation));
            
        }
        
	}
}
