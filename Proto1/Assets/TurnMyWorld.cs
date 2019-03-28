using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMyWorld : MonoBehaviour
{
    private GameObject gameObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(2, 0, 0);
    }
}
