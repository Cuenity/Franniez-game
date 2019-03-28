using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject balletje;
    // Start is called before the first frame update
    private void Awake()
    {
        //balletje = gameObject.AddComponent<Balletje>();
        balletje = Instantiate(balletje, new Vector3(1,3,1), Quaternion.identity);
    }
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {

    }
}
