using UnityEngine;

public class LookAndRotate : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private float _rotationSpeed;

    public void Start()
    {
        _rotationSpeed = -20f;
       
    }

    public void Update()
    {
        transform.LookAt(_target);
        //transform.Translate(Vector3.right * _rotationSpeed * Time.deltaTime);
        transform.RotateAround(_target.transform.position, Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}
