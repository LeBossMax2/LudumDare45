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
        int hs = PlayerPrefs.GetInt("SPOOKTOBERSURVIVAL_highScore", 0);
        int sc = PlayerPrefs.GetInt("SPOOKTOBERSURVIVAL_score", 0);
        killNumberText.text = "Highscore : " + hs + " -> " + Mathf.Max(hs,sc)+"\n"
            + "Actual score : " + sc + "\n"
            + "Damage per bullet : " + PlayerPrefs.GetInt("SPOOKTOBERSURVIVAL_dmg", 0);
        PlayerPrefs.SetInt("SPOOKTOBERSURVIVAL_highScore", Mathf.Max(hs,sc));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
