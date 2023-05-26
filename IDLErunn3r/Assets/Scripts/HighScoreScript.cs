using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    public GameObject scoreRank;
    public GameObject scoreName;
    public GameObject scoreValue;
    public GameObject scoreShipType;


    public void SetScore(string pRank, string pName, string pScore, string pType)
    {
        this.scoreRank.GetComponent<TextMeshProUGUI>().text = pRank;
        this.scoreName.GetComponent<TextMeshProUGUI>().text = pName;
        this.scoreValue.GetComponent<TextMeshProUGUI>().text = pScore;
        this.scoreShipType.GetComponent<TextMeshProUGUI>().text = pType;
    }
}
