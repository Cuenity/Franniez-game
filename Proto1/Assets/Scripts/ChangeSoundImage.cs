using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundImage : MonoBehaviour
{
    public Sprite SoundOn;
    public Sprite SoundOff;
    public Button TestButton;

    private Button button;
    private bool SoundState = true;

    // Start is called before the first frame update
    void Awake()
    {
        TestButton = GetComponent<Button>();

        // Only check if sound is off because the default image is on
        if(PlayerPrefs.GetInt("Sound") == 2)
        {
            TestButton.GetComponent<Image>().sprite = SoundOff;
            SoundState = false;
        }
    }

    public void ChangeImage()
    {
        if (SoundState)
        {
            TestButton.GetComponent<Image>().sprite = SoundOff;
            SoundState = false;
        }
        else
        {
            TestButton.GetComponent<Image>().sprite = SoundOn;
            SoundState = true; ;
        }
        
    }

    public void OnEnable()
    {
        MainMenu.ChangedSound += ChangeImage;
    }

}
