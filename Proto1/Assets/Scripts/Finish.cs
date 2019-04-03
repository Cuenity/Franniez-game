using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction Finished;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
