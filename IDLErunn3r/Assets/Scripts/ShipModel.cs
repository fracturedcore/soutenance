using UnityEngine;

[CreateAssetMenu(fileName = "ShipModel", menuName = "ship")]
public class ShipModel : ScriptableObject
{
    [SerializeField]
    public string _name;
    [SerializeField]
    public float _speed;
    [SerializeField]
    public float _health;
    [SerializeField]
    public float _fuel;
    [SerializeField]
    public string _highScoreBy;
    [SerializeField]
    public float _highScore;
    [SerializeField]
    public GameObject _shipPrefab;
   
}
