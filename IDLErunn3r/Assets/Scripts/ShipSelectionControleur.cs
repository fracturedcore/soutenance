using UnityEngine;

public class ShipSelectionControleur : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource1;
   
    void Start()
    {
        AudioManager.LoadSoundSettings();
        if (AudioManager._volumeMusiqueFloat == 0)
        {
            AudioManager._volumeMusiqueFloat = 0.5f;
        }
        
    }
    
    void Update()
    {
        _audioSource1.volume = AudioManager._volumeMusiqueFloat;
    }
}
