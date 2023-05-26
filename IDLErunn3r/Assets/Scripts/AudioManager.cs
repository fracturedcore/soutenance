using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string _firstPlayPref = "FirstPlay";
    private static readonly string _volumeMusiquePref = "VolumeMusique";
    private static readonly string _volumeSfxPref = "VolumeSfx";
    private int _firstPlayInt;
    [SerializeField]
    private Slider _volumeMusiqueSlider;
    [SerializeField]
    private Slider _volumeSfxSlider;
    [SerializeField]
    private AudioSource _musiqueAudioSource;
    [SerializeField]
    private AudioSource[] _sfxAudioSource;
    [SerializeField]
    private TextMeshProUGUI _volumeMusiqueText;
    [SerializeField]
    private TextMeshProUGUI _volumeSfxText;
    
    public static float _volumeMusiqueFloat;
    public static float _volumeSfxFloat;
    // Start is called before the first frame update
    void Start()
    {
        _volumeMusiqueText.text = Math.Floor(_volumeMusiqueSlider.value * 100) + " %";
        _volumeSfxText.text = Math.Floor(_volumeSfxSlider.value * 100) + " %";
        _firstPlayInt = PlayerPrefs.GetInt(_firstPlayPref);
        if (_firstPlayInt == 0)
        {
            _volumeMusiqueFloat = 0.5f;
            _volumeSfxFloat = 0.5f;
            _volumeMusiqueSlider.value = _volumeMusiqueFloat;
            _volumeSfxSlider.value = _volumeSfxFloat;
            PlayerPrefs.SetFloat(_volumeMusiquePref, _volumeMusiqueFloat);
            PlayerPrefs.SetFloat(_volumeSfxPref, _volumeSfxFloat);
            PlayerPrefs.SetInt(_firstPlayPref, -1);
        }
        else
        {
            _volumeMusiqueFloat = PlayerPrefs.GetFloat(_volumeMusiquePref);
            _volumeMusiqueSlider.value = _volumeMusiqueFloat;
            _volumeSfxFloat = PlayerPrefs.GetFloat(_volumeSfxPref);
            _volumeSfxSlider.value = _volumeSfxFloat;
        }
    }
    
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(_volumeMusiquePref, _volumeMusiqueSlider.value);
        _volumeMusiqueFloat = _volumeMusiqueSlider.value;
        PlayerPrefs.SetFloat(_volumeSfxPref, _volumeSfxSlider.value);
        _volumeSfxFloat = _volumeSfxSlider.value;
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveSoundSettings();
        }
    }
    public static void LoadSoundSettings()
    {
        _volumeMusiqueFloat = PlayerPrefs.GetFloat(_volumeMusiquePref);
        _volumeSfxFloat = PlayerPrefs.GetFloat(_volumeSfxPref);
    }
    public void Update()
    {
        _volumeMusiqueText.text = Math.Floor(_volumeMusiqueSlider.value * 100) + " %";
        _volumeSfxText.text = Math.Floor(_volumeSfxSlider.value * 100) + " %";
       UpdateSounds();
    }

    public void UpdateSounds()
    {
        _musiqueAudioSource.volume = _volumeMusiqueSlider.value;
        foreach (var sfx in _sfxAudioSource)
        {
            sfx.volume = _volumeSfxSlider.value;
        }
    }
}
