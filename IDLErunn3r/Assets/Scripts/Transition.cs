using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public Image imgTransition;
    public float colorRed = 0f;
    public float colorGreen = 0f;
    public float colorBlue = 0f;
    public float colorAlpha = 1f;
    public bool bIsTransitionToBlack;
    public string sceneName;

    void Start()
    {
        bIsTransitionToBlack = false;
        imgTransition = GetComponent<Image>();
        imgTransition.color = new Color(colorRed, colorGreen, colorBlue, colorAlpha);
        imgTransition.raycastTarget = true;
    }
    
    void Update()
    {
        if (bIsTransitionToBlack)
        {
            if (imgTransition.color.a <= 1)
            {
                colorAlpha += 0.9f * Time.deltaTime;
                imgTransition.color = new Color(colorRed, colorGreen, colorBlue, colorAlpha);
                imgTransition.raycastTarget = true;
                if (imgTransition.color.a >= 1)
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
        else
        {
            if (imgTransition.color.a >= 0)
            {
                colorAlpha -= 0.9f * Time.deltaTime;
                imgTransition.color = new Color(colorRed, colorGreen, colorBlue, colorAlpha);
                if (imgTransition.color.a <= 0)
                {
                    imgTransition.raycastTarget = false;
                }
            }
        }
    }

    public void StartTransition(string pNomScene)
    {
        sceneName = pNomScene;
        bIsTransitionToBlack = true;
        imgTransition.raycastTarget = false;
    }
    
}
