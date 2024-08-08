using UnityEngine;
using UnityEngine.UI;
// using Unity.VisualScripting;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public GameObject OFFButton;
    public GameObject ONButton;
    public AudioSource clickSFX;

    private bool isMuted = false;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        //Load User's last Music Volume change:
        if (!PlayerPrefs.HasKey("musicVolume") && !PlayerPrefs.HasKey("ONButton") && !PlayerPrefs.HasKey("OFFButton"))
        {
            Save(0.35f);
            PlayerPrefs.SetInt("ONButton", 1);
            PlayerPrefs.SetInt("OFFButton", 0);
        }
        Load();
    }

    //Change MusicVolume:
    public void SetMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volumeSlider.value) * 20);
        Save(volumeSlider.value);
    }

    public void Reset()
    {
        ONButton.SetActive(true);
        OFFButton.SetActive(false);
        SetunMuted();
        Save(0.35f);
        Load();
    }

    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        if (PlayerPrefs.GetInt("ONNButton") == 1)
        {
            isMuted = false;
            ONButton.SetActive(true);
            OFFButton.SetActive(false);
        }
        if (PlayerPrefs.GetInt("OFFButton") == 1)
        {
            isMuted = true;
            ONButton.SetActive(false);
            OFFButton.SetActive(true);
        }
    }

    //Save the Player's volume change.
    public void Save(float volumeValue)
    {
        PlayerPrefs.SetFloat("musicVolume", volumeValue);
    }

    public void SetMuted()
    {
        isMuted = true;
        PlayerPrefs.SetInt("ONButton", 0);
        PlayerPrefs.SetInt("OFFButton", 1);

    }

    public void SetunMuted()
    {
        isMuted = false;
        PlayerPrefs.SetInt("ONButton", 1);
        PlayerPrefs.SetInt("OFFButton", 0);

    }

    public void Playclick()
    {
        if (isMuted == false)
        {
            clickSFX.Play();
        }
    }

}
