using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour {

    Button button;
    // Use this for initialization
    private void Awake()
    {
        button = this.GetComponent<Button>();
    }

    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        button.onClick.AddListener(clicked);
	}

    private void clicked()
    {
        Debug.Log("KLIKKKKK");
    }
}
