using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.position += new Vector3(0, 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.position += new Vector3(0, -0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(-0.5f, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(0.5f,0);
        }
    }
}
