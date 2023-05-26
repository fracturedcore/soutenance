using UnityEngine;

public class ShipControl : MonoBehaviour
{
    private const float _MAXX = 8;
    private const float _MAXY = 2;
    private const float _MINX = -8;
    private const float _MINY = 0.2f;

    private float translationSpeed = 20;
  
    private float elvationSpeed = 9;
  
    private float fallSpeed = 9f;
    
    private float moveSpeed = ShipSelection.speed;
    private float currentSpeed = 0;
    
    private float acceleration = 20f;
    
    private float deceleration = 30f;
    private ParticleSystem _particleLeft;
    private ParticleSystem _particleRight;
    [SerializeField]
    private bool _bIsAccelerate;
    private bool _bIsFlying;
    public int fioulLoosValue;
   
    public static bool hitWall = false;
    public static bool dead = false;
    public static bool hitLava = false;
   public static bool refill = false;
    void Start()
    {
        _particleLeft = GameObject.Find("ReactorParticleLeft").GetComponent<ParticleSystem>();
        _particleRight = GameObject.Find("ReactorParticleRight").GetComponent<ParticleSystem>();
        fioulLoosValue = 1;
        //Debug.Assert(_particleLeft != null, "Particle Left not found");
        //Debug.Assert(_particleRight != null, "Particle Right not found");
        transform.position = new Vector3(0, 0, 0);
        _bIsAccelerate = false;
        _bIsFlying = false;
    }

    public void ChangeReactorFlameSize(ParticleSystem pParticules, bool pBIsAccelerate)
    {
        if(pBIsAccelerate)
        {
            var main = pParticules.main;
            main.startSpeed = 2f;
            main.startSize = 1.25f;
        }
        else
        {
            var main = pParticules.main;
            main.startSpeed = 0.5f;
            main.startSize = 1f;
        }
    }
    void Update()
    {
       
       hitWall = false;
       dead = false;
       hitLava = false;
       refill = false;
       
        
        //elevation && chute
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y <= _MAXY && GameControleur.health > 0 && GameControleur.fioul > 0)
        {
            _bIsFlying = true;
            transform.position += transform.up * elvationSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.UpArrow) == false && transform.position.y is >= _MINY && GameControleur.health > 0 && GameControleur.fioul > 0)
        {
            _bIsFlying = false;
            transform.position -= transform.up * fallSpeed * Time.deltaTime;
        }
        //translation gauche droite
        if(Input.GetKey(KeyCode.RightArrow) && transform.position.x < _MAXX && GameControleur.health > 0 && GameControleur.fioul > 0)
        {
            transform.position += transform.right * translationSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow) && transform.position.x > _MINX  && GameControleur.health > 0 && GameControleur.fioul > 0)
        {
            transform.position -= transform.right * translationSpeed * Time.deltaTime;
        }
        //acceleration && deceleration
        if (Input.GetKey(KeyCode.Mouse0) && GameControleur.health > 0 && GameControleur.fioul > 0)
        {
            ScoreManager.AddScore(1);
            currentSpeed += acceleration;
            if (currentSpeed >= moveSpeed)
            {
                currentSpeed = moveSpeed;
            }
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
            if(!_bIsAccelerate)
            {
                _bIsAccelerate = true;
                ChangeReactorFlameSize(_particleLeft, _bIsAccelerate);
                ChangeReactorFlameSize(_particleRight, _bIsAccelerate);
            }
        }
        else
        {
            currentSpeed -= deceleration;
            if (currentSpeed <= 0)
            {
                currentSpeed = 0;
            }
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
            if(_bIsAccelerate)
            {
                _bIsAccelerate = false;
                ChangeReactorFlameSize(_particleLeft, _bIsAccelerate);
                ChangeReactorFlameSize(_particleRight, _bIsAccelerate);
            }
        }
        
        if (!_bIsFlying)
        {
            fioulLoosValue = 1;
        }
        else if (_bIsFlying)
        {
            fioulLoosValue = 2;
        }
        GameControleur.RemoveFioul(fioulLoosValue * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("collide"))
        {
            
            if (ScoreManager.score >= 1000)
            {
                ScoreManager.RemoveScore(1000);
            }
            else
            {
                ScoreManager.score = 0;
            }
            GameControleur.RemoveLife(10);

            GameControleur.RemoveFioul(4);

            if (GameControleur.health > 0 && GameControleur.fioul > 0)
            {
                hitWall = true;
            }
            else
            {
                dead = true;
            }
            
        }
        
        
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("lava"))
        {
            if (ScoreManager.score >= 10)
            {
                ScoreManager.RemoveScore(10);
            }
            else
            {
                ScoreManager.score = 0;
            }
            GameControleur.RemoveLife(10 * Time.deltaTime);
            GameControleur.RemoveFioul(1 * Time.deltaTime);
            if (GameControleur.health > 0 && GameControleur.fioul > 0)
            {
                hitLava = true;
            }
            else
            {
                dead = true;
            }
        }
        
        if (other.gameObject.CompareTag("refill"))
        {
            refill = true;
            if (GameControleur.fioul < GameControleur.maxFioul)
            {
                GameControleur.AddFioul(100 * Time.deltaTime);
            }

        }
    }
}
