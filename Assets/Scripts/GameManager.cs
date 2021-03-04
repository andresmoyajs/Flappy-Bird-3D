using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;

    public int score = 0;
    private int highScore = 0;
    private static GameManager manager;

    private static int currentScene = 1;

    public static GameManager Instance
    {
        get
        {
            if (manager == null)
            {
                manager = GameObject.FindObjectOfType<GameManager>();
                if (manager == null)
                {
                    var newObj = new GameObject();
                    manager = newObj.AddComponent<GameManager>();
                }
            }

            return manager;
            
        }
    }

    public UnityEvent OnPlayerDeath;
    public IntEvent OnScoreChange;

    public int HighScore
    {
        get
        {
            if (score > highScore)
            {
                return score;
            }
            return highScore;

        }
    }

    public int CurrentScene
    {
        get
        {
            return currentScene;

        }
    }

    public void ResetCurrentScene()
    {
        currentScene = 1;
    }

    public int Score
    {
        get
        {
            return score;

        }
    }

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }

        if (manager != this)
        {
            Destroy(this.gameObject);
        }

        OnPlayerDeath = new UnityEvent();
        OnScoreChange = new IntEvent();

        highScore = SaveSystem.LoadScore();
        OnPlayerDeath.AddListener(OnPlayerDeathEvent);

    }

    public void AddjustScore(int adjustment)
    {
        score += adjustment;
        OnScoreChange.Invoke(score);

        if (score == 10 && currentScene != 3)
        {
            score = 0;

            currentScene++;

            if (currentScene > 3)
            {
                currentScene = 1;
            }
            Invoke("NextScene", 0.2f);

        }
    }

    private void NextScene()
    {
        SceneManager.LoadScene("Escenario " + currentScene.ToString());
    }

    private void OnPlayerDeathEvent()
    {

        if (score > highScore)
        {
            SaveSystem.SaveScore(score);
        }
    }
}

[System.Serializable]
public class IntEvent : UnityEvent<int>
{

}
