using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverControleur : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource1;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.LoadSoundSettings();
        if (AudioManager._volumeMusiqueFloat == 0)
        {
            AudioManager._volumeMusiqueFloat = 0.5f;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        _audioSource1.volume = AudioManager._volumeMusiqueFloat;
    }
}
