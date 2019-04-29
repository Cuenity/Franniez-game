using Assets.Scripts.Shop.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopFillSkins : MonoBehaviour
{
    [SerializeField]
    private ShopCategory shopCategory;
    [SerializeField]
    private Button button;
    [SerializeField]
    private SimpleObjectPool simpleObjectPool;
    [SerializeField]
    private Transform contentPanel;
    [SerializeField]
    private GameObject marginRight;

    private List<Button> buttons;

    public delegate void ClickAction(SkinObject cost);
    public event ClickAction ButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        MakeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MakeButtons()
    {
        foreach (SkinObject skin in shopCategory.skins)
        {
            GameObject newButton = simpleObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            ShopSkinButton skinButton = newButton.GetComponent<ShopSkinButton>();
            skinButton.Setup(skin);
            skinButton.skinButton.onClick.AddListener(delegate { BuySkin(skin); });
        }

        marginRight = (GameObject)GameObject.Instantiate(marginRight);
        marginRight.SetActive(true);
        marginRight.transform.SetParent(contentPanel);
    }

    void BuySkin(SkinObject skin)
    {
        ButtonClicked(skin);
    }
}
