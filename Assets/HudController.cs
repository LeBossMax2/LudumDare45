using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public Text timer;
    public Text hp;
    public Text kills;

    public Controller player;

    private void Update()
    {
        int time = (int)Time.realtimeSinceStartup;
        timer.text = (time/60).ToString("D2") + ":" + (time % 60).ToString("D2");
        hp.text = player.current_healthPoint + " / " + player.max_healthPoint;
        kills.text = Controller.killCount.ToString();
    }
}
