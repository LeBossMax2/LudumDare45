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
        int hs = PlayerPrefs.GetInt("highScore", 0);
        int sc = PlayerPrefs.GetInt("score", 0);
        killNumberText.text = "Highscore : " + hs + " -> " + Mathf.Max(hs,sc)+"\n"
            + "Actual score : " + sc;
        PlayerPrefs.SetInt("highScore", Mathf.Max(hs,sc));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
