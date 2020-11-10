using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public Text timer;
    public Text hp;
    public Text kills;
    public Text pause;

    public static int maxNumberEnnemies = 70;
    public static int ennemiesCount = 0;
    public Text ennemiesCounter;

    public Controller player;
    public static int currentWaveNumber = 0;

    // to delete later
    public bool newWave = true;

    private void Awake()
    {
        ennemiesCount = 0;
    }

    private void Update()
    {
        ennemiesCounter.text = ""+ennemiesCount;

        int time = (int)Time.timeSinceLevelLoad;

        timer.text = (time/60).ToString("D2") + ":" + (time % 60).ToString("D2");
        hp.text = player.current_healthPoint + " / " + player.max_healthPoint;
        kills.text = Controller.killCount.ToString();
    }
}
