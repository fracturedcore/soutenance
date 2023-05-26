using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameControleur : MonoBehaviour
{
    public static GameControleur instance;
    [SerializeField]
    private GameObject[] _groundsPrefabs;
    [SerializeField]
    private GameObject[] _groundInGame;
    [SerializeField]
    private GameObject[] _shipPrefab;
    [SerializeField]
    private GameObject _shipInGame;
    public static float health;
    public static int maxHealth;
    public static float fioul;
    public static float maxFioul;
    private GameObject _ship;
    private float _groundSize;
    private float _shipMargin = 6f; 
    [SerializeField]
    private Image _healthBarRed;
    [SerializeField]
    private Image _healthBarGreen;
    [SerializeField]
    private Image _energieBarBlue;
    [SerializeField]
    private Image _energieBarWhite;
    [SerializeField]
    private  AudioSource _audioSource1;
    [SerializeField]
    public  AudioSource _audioSource2;
    [SerializeField] 
    public  AudioClip[] _lstsounds;
    private static bool _bIsDeadOrOutOfFioul;
    private static float _timerGO;
    private int _NbGround = 5;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
           
        }
        instance = this;
    }
    void Start()
    { 
        AudioManager.LoadSoundSettings();
        _timerGO = 0;
        _bIsDeadOrOutOfFioul = false;
        health = ShipSelection.health;
        maxHealth = ShipSelection.health;
        fioul = ShipSelection.fuel;
        maxFioul = ShipSelection.fuel;
        _ship = GameObject.Find("Ship");
        
        _groundInGame = new GameObject[_NbGround];
        _shipInGame = Instantiate(_shipPrefab[ShipSelection.model]);
        _shipInGame.AddComponent<ShipControl>();
        
        _shipInGame.transform.localScale = new Vector3(1,1,1);
        
        for (int i = 0; i < _groundInGame.Length; i++)
        {
            int n = Random.Range(0, 9);
            _groundInGame[i] = Instantiate(_groundsPrefabs[n]);
        }
        _groundSize = _groundInGame[0].GetComponentInChildren<Transform>().Find("Road").localScale.z;
        
        float pos = _ship.transform.position.z + (_groundSize / 2) - 1.5f;
        foreach (var road in _groundInGame)
        {
            road.transform.position = new Vector3(0, 0, pos);
            pos += _groundSize ;
        }
        _audioSource2.clip = _lstsounds[0];
        _audioSource2.Play();
       
    }
    
    void Update()
    {
        _audioSource1.volume = AudioManager._volumeMusiqueFloat;
        _audioSource2.volume = AudioManager._volumeSfxFloat;
        if (_audioSource2.isPlaying == false)
        {
            _audioSource2.clip = _lstsounds[0];
            _audioSource2.Play();
            _audioSource2.loop = true;
        }
        for (int i = _groundInGame.Length - 1; i >= 0; i--)
        {
            var ground = _groundInGame[i];
            var newGround = 0;
            if (ground.transform.position.z + (_groundSize / 2) < _ship.transform.position.z - _shipMargin)
            {
                var oldGroundPosition = ground.transform.position.z;
                Destroy(ground);
                if (ScoreManager.score >= 0 && ScoreManager.score <= 10000)
                {
                    newGround = Random.Range(0, 9);
                    ground = Instantiate(_groundsPrefabs[newGround]);
                    ground.transform.position = new Vector3(0, 0, oldGroundPosition + _groundSize * _NbGround);
                    _groundInGame[i] = ground;
                }
                else if (ScoreManager.score > 10000 && ScoreManager.score <= 20000)
                {
                     newGround = Random.Range(0, 11);
                    ground = Instantiate(_groundsPrefabs[newGround]);
                    ground.transform.position = new Vector3(0, 0, oldGroundPosition + _groundSize * _NbGround);
                    _groundInGame[i] = ground;
                }
                else if (ScoreManager.score > 20000 && ScoreManager.score <= 30000)
                {
                     newGround = Random.Range(0, 14);
                    ground = Instantiate(_groundsPrefabs[newGround]);
                    ground.transform.position = new Vector3(0, 0, oldGroundPosition + _groundSize * _NbGround);
                    _groundInGame[i] = ground;
                }
                else if (ScoreManager.score > 30000)
                {
                    newGround = Random.Range(0, 15);
                    ground = Instantiate(_groundsPrefabs[newGround]);
                    ground.transform.position = new Vector3(0, 0, oldGroundPosition + _groundSize * _NbGround);
                    _groundInGame[i] = ground;
                }
            }
        }
        _healthBarGreen.rectTransform.sizeDelta = new Vector2(_healthBarRed.rectTransform.sizeDelta.x * health / maxHealth ,_healthBarRed.rectTransform.sizeDelta.y);
        _energieBarBlue.rectTransform.sizeDelta = new Vector2(_energieBarWhite.rectTransform.sizeDelta.x * fioul / maxFioul ,_energieBarWhite.rectTransform.sizeDelta.y);

        if (_bIsDeadOrOutOfFioul)
        {
            _timerGO += Time.deltaTime;
            if (_timerGO >= 2)
            {
                SceneManager.LoadScene("SceneGameOver");
            }
        }

        if (ShipControl.hitWall)
        {
            if (_audioSource1.isPlaying == false)
            {
                _audioSource1.clip = _lstsounds[1];
                _audioSource1.Play();
            }
            
        }
        
        if (ShipControl.dead)
        {
            if (_audioSource1.isPlaying == false)
            {
                _audioSource1.clip = _lstsounds[2];
                _audioSource1.Play();  
            }
          
        }

        if (ShipControl.hitLava)
        {
           
            if (_audioSource1.isPlaying == false)
            {
                _audioSource1.clip = _lstsounds[1];
                _audioSource1.Play();
            }
            
        }
       
        if (ShipControl.refill)
        {
            if (_audioSource1.isPlaying == false)
            {
                _audioSource1.clip = _lstsounds[3];
                _audioSource1.Play();
            }
            
        }
    }
    private void OnDestroy()
    {
        instance = null;
    }
    
    public static void  RemoveLife(float pDamage)
    {
        health -= pDamage;
        if (health <= 0)
        {
            health = 0;
            _bIsDeadOrOutOfFioul = true;
        }
    }
    public static void AddLife(float pValue)
    {
        health += pValue;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public static void RemoveFioul(float pFioulLost)
    {
        fioul -= pFioulLost;
        
        if (fioul <= 0)
        {
            fioul = 0;
            _bIsDeadOrOutOfFioul = true;
        }
    }

    public static void AddFioul(float pValue)
    {
        fioul += pValue;
        
        if (fioul >= maxFioul)
        {
            fioul = maxFioul;
        }
    }
}
