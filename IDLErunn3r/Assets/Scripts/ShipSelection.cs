using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipSelection : MonoBehaviour
{
   [SerializeField]
   private ShipModel[] _shipModels;
   [SerializeField]
   private Transform _spot;
   [SerializeField]
   private TextMeshProUGUI _shipName;
   [SerializeField]
   private TextMeshProUGUI _shipSpeed;
   [SerializeField]
   private TextMeshProUGUI _shipHealth;
   [SerializeField]
   private TextMeshProUGUI _shipFuel;
   [SerializeField]
   private List<GameObject> _ships;
   public static int _currentShipIndex;
   public static int model;
   public static int speed;
   public static int health;
   public static int fuel;
   public static string shipType;
   [SerializeField]
   private AudioSource _audioSource1;
   
   public void Start()
   {
      _ships = new List<GameObject>();
      _shipName.text = "TYPE : " + _shipModels[_currentShipIndex]._name;
      _shipSpeed.text = "SPEED : " + _shipModels[_currentShipIndex]._speed;
      _shipHealth.text = "HEALTH : " + _shipModels[_currentShipIndex]._health;
      _shipFuel.text = "FUEL : " + _shipModels[_currentShipIndex]._fuel;
      foreach (var shipModel in _shipModels)
      {
         GameObject go = Instantiate(shipModel._shipPrefab, _spot.position, Quaternion.identity);
         ParticleSystem _particleLeft = GameObject.Find("ReactorParticleLeft").GetComponent<ParticleSystem>();
         ParticleSystem _particleRight = GameObject.Find("ReactorParticleRight").GetComponent<ParticleSystem>();
         _ships.Add(go);
         go.SetActive(false);
         go.transform.localScale = new Vector3(50,50,50);
         _particleLeft.transform.localScale = new Vector3(50,50,50);
         _particleRight.transform.localScale = new Vector3(50,50,50);
      }
      showShipFromList();
   }

   public void Update()
   {
      _shipName.text = "TYPE : " + _shipModels[_currentShipIndex]._name;
      _shipSpeed.text = "SPEED : " + _shipModels[_currentShipIndex]._speed;
      _shipHealth.text = "HEALTH : " + _shipModels[_currentShipIndex]._health;
      _shipFuel.text = "FUEL : " + _shipModels[_currentShipIndex]._fuel;
      shipType = _shipModels[_currentShipIndex]._name;
   }

   public void showShipFromList()
   {

      _ships[_currentShipIndex].SetActive(true);
   }
   public void OnClickNext()
   {  
      _ships[_currentShipIndex].SetActive(false);
      
      if (_currentShipIndex < _ships.Count - 1)
      {
         _currentShipIndex += 1;
      }
      else
      {
         _currentShipIndex = 0;
      }
      _audioSource1.Play();
      showShipFromList();
   }
   public void OnClickPrevious()
   {
      _ships[_currentShipIndex].SetActive(false);
      
      if (_currentShipIndex == 0)
      {
         _currentShipIndex = _ships.Count - 1;
      }
      else
      {
         _currentShipIndex -= 1;
      }
      _audioSource1.Play();
      showShipFromList();
   }
   public void OnClickPlay()
   {
      model = _currentShipIndex;
      health = (int)_shipModels[_currentShipIndex]._health;
      speed = (int)_shipModels[_currentShipIndex]._speed;
      fuel = (int)_shipModels[_currentShipIndex]._fuel;
      var imgTransition = GameObject.Find("ImgTransition");
      Transition transition = imgTransition.GetComponent<Transition>();
      transition.StartTransition("SceneGameplay");
      ScoreManager.score = 0;
      ScoreManager.highScore = ScoreManager.highScore;
      _audioSource1.Play();
   }
}
