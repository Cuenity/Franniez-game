using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructieBehaviour : MonoBehaviour {

    private Text thisText;
    private int score;

    public int MaxBlocks = 10;
    private static int BlocksUsed;

    void Start()
    {
        thisText = GetComponent<Text>();
        
    }

    void Update()
    {
        thisText.text = "Used Are" + BlocksUsed + "Out of" + MaxBlocks;
    }

    public static void PlaceBlock()
    {
        BlocksUsed = BlocksUsed + 1;
        
    }
}
