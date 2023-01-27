using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHighestRound : MonoBehaviour
{
    public TextMeshProUGUI highestRoundTxt;
    public int highestRound;

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
        
    }
}
