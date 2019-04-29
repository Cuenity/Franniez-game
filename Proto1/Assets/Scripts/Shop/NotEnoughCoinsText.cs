using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughCoinsText : MonoBehaviour
{
    [SerializeField]
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        GetComponentInParent<ShopButtons>().BuySkinEvent += SetText;
    }

    public void SetText(string amount)
    {
        text.text = PlayerDataController.instance.player.ShopCoins.ToString() + "/" + amount;
    }
}
