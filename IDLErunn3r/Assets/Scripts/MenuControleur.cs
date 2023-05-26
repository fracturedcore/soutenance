using UnityEngine;

public class MenuControleur : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource1;
    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager._volumeMusiqueFloat == 0)
        {
            AudioManager._volumeMusiqueFloat = 0.5f;
        }
        AudioManager.LoadSoundSettings();
    }

    
    // Update is called once per frame
    void Update()
    {
        _audioSource1.volume = AudioManager._volumeMusiqueFloat;
    }
}
