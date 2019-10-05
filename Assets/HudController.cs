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
        timer.text = ((int)Time.realtimeSinceStartup).ToString();
        hp.text = player.current_healthPoint.ToString();
        kills.text = player.killCount.ToString();
    }
}
