using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMusic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip World1, World2, World3;
    void Start()
    {
        WorldManager worldManager = new WorldManager();
        switch (worldManager.SetWorldMusic())
        {
            case 1:
                gameObject.GetComponent<AudioSource>().clip = World1;
                break;
            case 2:
                gameObject.GetComponent<AudioSource>().clip = World2;
                break;
            case 3:
                gameObject.GetComponent<AudioSource>().clip = World3;
                break;
            default:
                break;
        }
        gameObject.GetComponent<AudioSource>().Play();
    }
}
