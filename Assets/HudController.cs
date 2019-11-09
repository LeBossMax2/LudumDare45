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

    public static int ennemiesCount = 0;
    public Text ennemiesCounter;

    public Controller player;

    public SpawnWave[] list_spawns;
    public static int currentWaveNumber = 1;
    public int WaveDuration = 60;

    // to delete later
    public bool newWave = true;

    private void Awake()
    {
        currentWaveNumber = 0;
        ennemiesCount = 0;
    }

    private void Update()
    {
        ennemiesCounter.text = ""+ennemiesCount;

        int time = (int)Time.timeSinceLevelLoad;

        // Condition to start a new wave
        if(0 == (time % 60) && newWave)
        {
            newWave = false;
            currentWaveNumber++;
            for(int i=0;i<list_spawns.Length;i++)
            {
                list_spawns[i].restart();
                list_spawns[i].localWaveNumber++;
            }
        }
        if(0 != (time % 60))
        {
            newWave = true;
        }
        timer.text = (time/60).ToString("D2") + ":" + (time % 60).ToString("D2");
        hp.text = player.current_healthPoint + " / " + player.max_healthPoint;
        kills.text = Controller.killCount.ToString();

        pause.text = Time.timeScale == 0 ? "PAUSE" : "";
    }
}
