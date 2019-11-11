using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOverTexts : MonoBehaviour
{
    public Text killNumberText;
    // Start is called before the first frame update
    void Start()
    {
        killNumberText.text = "Highscore : " + PlayerPrefs.GetInt("highScore",0) + "\n"
            + "Actual score : " + PlayerPrefs.GetInt("score",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
