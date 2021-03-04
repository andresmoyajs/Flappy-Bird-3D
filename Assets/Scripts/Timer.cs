using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeInGame;

    private string sec = string.Empty;
    private string min = string.Empty;

    private static float time = 0.0f;
    public static int minutes;
    public static int seconds;
    private int bestTime = 0;
    private int totalSec = 0;

    private static bool startTimer = false;

    public static Timer instance;

    public static Timer GetInstance()
    {
        if (instance == null)
        {
            instance = Timer.FindObjectOfType<Timer>();
            var timer = new GameObject();
            instance = timer.AddComponent<Timer>();
        }
        return instance;
    }

    public int GetTime()
    {
        return totalSec;
    }

    public string BestTime
    {
        get
        {
            int hours;
            int minutes;
            int seconds;

            bestTime = SaveSystem.LoadScore();

            if (totalSec < bestTime)
            {
                hours = (totalSec / 3600);
                minutes = ((totalSec - hours * 3600) / 60);
                seconds = (totalSec - (hours * 3600 + ((totalSec - hours * 3600) / 60) * 60));
            }

            hours = (bestTime / 3600);
            minutes = (bestTime - hours * 3600) / 60;
            seconds = bestTime - (hours * 3600 + ((bestTime - hours * 3600) / 60) * 60);

            string min = minutes.ToString();
            string sec = seconds.ToString();

            if (minutes < 10)
            {
                min = "0" + minutes.ToString();
            }

            if (seconds < 10)
            {
                sec = "0" + Mathf.RoundToInt(seconds).ToString();
            }

            return min + ":" + sec;
        }
    }

    public string GetTimeString()
    {
        if(min == string.Empty)
        {
            min = "00";
        }

        if (sec == string.Empty)
        {
            sec = "00";
        }
        return min + ":" + sec;
    }


    public void SetTextMesh(TextMeshProUGUI timeInGame)
    {
        this.timeInGame = timeInGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            time += Time.deltaTime;
            minutes = (int)Mathf.Floor(time / 60);
            seconds = (int)time % 60;

            min = minutes.ToString();
            sec = seconds.ToString();

            if (minutes < 10)
            {
                min = "0" + minutes.ToString();
            }
            if (seconds < 10)
            {
                sec = "0" + Mathf.RoundToInt(seconds).ToString();
            }

            timeInGame.text = min + ":" + sec;

        }
    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void StopTimer()
    {
        int minsToSec = minutes * 60;
        totalSec = minsToSec + seconds;
        bestTime = SaveSystem.LoadScore();

        if(totalSec < bestTime)
        {
            SaveSystem.SaveScore(totalSec);
        }

        time = 0;
        startTimer = false;
    }


}
