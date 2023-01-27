using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highestRoundTxt;
    public int highestRound = 0;
    // Start is called before the first frame update
    void Start()
    {
        highestRoundTxt = GetComponent<TextMeshProUGUI>();
        highestRound = PlayerPrefs.GetInt("Highest Round", 0);
        highestRoundTxt.text = "Highest Round: " + highestRound.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(highestRound < UIManager.instance.roundNum)
        {
            highestRound = UIManager.instance.roundNum;
            PlayerPrefs.SetInt("Highest Round", highestRound);
        }

        highestRoundTxt.text = "Highest Round: " + highestRound.ToString();
    }
}
