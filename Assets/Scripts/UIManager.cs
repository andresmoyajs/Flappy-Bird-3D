using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI endScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;


    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI bestTime;


    public AudioClip musicFx;
    public AudioClip musicFall;

    public static UIManager instance;

    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            instance = UIManager.FindObjectOfType<UIManager>();
            var gameObject = new GameObject();
            instance = gameObject.AddComponent<UIManager>();
        }
        return instance;
    }


    private void Start()
    {
        //GameManager.Instance.OnScoreChange.AddListener(OnScoreChange);
        GameManager.Instance.OnPlayerDeath.AddListener(OnPlayerDeath);
    }

    public void OnPlayerDeath()
    {
        if (endScreen.activeSelf == false){

            var timer = Timer.GetInstance();
            Debug.Log(timer.BestTime);
            time.text = timer.GetTime().ToString();

            bestTime.text = time.text;
            
            //endScoreText.text = scoreText.text;
            //scoreText.enabled = false;

            AudioManager.current.PlaySound(musicFx);
            Invoke("FallSound", 0.5f);
            Invoke("ResetScenes", 1.5f);
        }
        
    }

    public void ResetLevel()
    {
        GameManager.Instance.ResetCurrentScene();
        SceneManager.LoadScene(0);
    }

    private void OnScoreChange(int score)
    {
        time.text = score.ToString();
    }

    private void ResetScenes()
    {
        if (GameManager.Instance.CurrentScene == 1)
        {
            SceneManager.LoadScene(0);
        }

        if (GameManager.Instance.CurrentScene == 2)
        {
            SceneManager.LoadScene(1);
        }

        if (GameManager.Instance.CurrentScene == 3)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void FallSound()
    {
        AudioManager.current.PlaySound(musicFall);
    }

}
