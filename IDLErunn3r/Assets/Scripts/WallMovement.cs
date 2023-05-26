using UnityEngine;

public class WallMovement : MonoBehaviour
{
    private const float _MAXX = 9.5f;
    private const float _MINX = -9.5f;
    [SerializeField]
    private float translationSpeed = 2f;
    
    void Update()
    {
        
        transform.position += transform.right * translationSpeed * Time.deltaTime;
        if (transform.position.x <= _MAXX)
        {
            translationSpeed = -translationSpeed;
        }
        if (transform.position.x >= _MINX)
        {
            translationSpeed = -translationSpeed;
        }
        
        
    }
   
    
}
