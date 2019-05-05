using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundImage : MonoBehaviour
{
    [SerializeField] private Sprite SoundOn, SoundOff;

    [SerializeField] private Button SoundButton;

    private bool SoundState = true;

    void Awake()
    {
        if(this != null)
        {
            SoundButton = GetComponent<Button>();
        }

        // Only check if sound is off because the default image is on
        if(PlayerPrefs.GetInt("Sound") == 2)
        {
            SoundButton.GetComponent<Image>().sprite = SoundOff;
            SoundState = false;
        }
    }

    // Change image
    public void ChangeImage()
    {
        if (SoundState)
        {
            SoundButton.GetComponent<Image>().sprite = SoundOff;
            SoundState = false;
        }
        else
        {
            SoundButton.GetComponent<Image>().sprite = SoundOn;
            SoundState = true; ;
        }
    }

    // Add ChangeImage method to Trigger ChangedSound (Main Menu)
    public void OnEnable()
    {
        GetComponentInParent<MainMenu>().ChangedSound += ChangeImage;
    }
}
