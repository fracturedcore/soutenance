using TMPro;
using UnityEngine;

public class BtnClick : MonoBehaviour
{
    public string pseudo;
    [SerializeField]
    private AudioSource _audioSource1;
    public void OnClickQuit()
    {
        _audioSource1.Play();
        Application.Quit();
    }
   
    public void OnClickMenu()
    {
        var imgTransition = GameObject.Find("ImgTransition");
        var transition = imgTransition.GetComponent<Transition>();
        transition.StartTransition("SceneMenu");
        _audioSource1.Play();
    }
    
    public void OnClickMenuGameOver()
    {
        pseudo = GameObject.Find("enterPseudo").GetComponent<TMP_InputField>().text;
        
        if (pseudo == "")
        {
            pseudo = "Anonyme";
        }
        HighScoreManager.AddScore(pseudo, ScoreManager.score, ShipSelection.shipType);
        var imgTransition = GameObject.Find("ImgTransition");
        var transition = imgTransition.GetComponent<Transition>();
        transition.StartTransition("SceneMenu");
        _audioSource1.Play();
    }
    
    public void OnClickOptions()
    {
        var imgTransition = GameObject.Find("ImgTransition");
        var transition = imgTransition.GetComponent<Transition>();
        transition.StartTransition("SceneOption");
        _audioSource1.Play();
    }
    public void OnClickShipSelection()
    {
        var imgTransition = GameObject.Find("ImgTransition");
        var transition = imgTransition.GetComponent<Transition>();
        transition.StartTransition("SceneShipSelection");
        _audioSource1.Play();
    }
    
    public void OnClickHighScore()
    {
        var imgTransition = GameObject.Find("ImgTransition");
        var transition = imgTransition.GetComponent<Transition>();
        transition.StartTransition("SceneHighScore");
        _audioSource1.Play();
    }
}
