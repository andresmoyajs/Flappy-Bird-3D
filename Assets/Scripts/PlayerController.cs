using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRB;
    [SerializeField] private ForceMode forceMode;
    [SerializeField] private float force;
    [SerializeField] private float gravity;
    [SerializeField] private float maxHeightThreshold;

    public TextMeshProUGUI time;
    public TextMeshProUGUI bestTime;

    public TextMeshProUGUI timeInGame;
    public TextMeshProUGUI scoreInGame;

    private bool playerIsAlive = true;
    // Start is called before the first frame update
    private Transform birdMode;

    public GameObject spawnerTunnel;
    public GameObject startTxt;

    public AudioClip musicFx;

    public GameObject endScreen;


    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        GameManager.Instance.OnPlayerDeath.AddListener(OnPlayerDeath);
        birdMode = transform.GetChild(0);
        spawnerTunnel.SetActive(false);
        startTxt.SetActive(true);

        var timer = Timer.GetInstance();
        timer.SetTextMesh(timeInGame);
        timeInGame.text = timer.GetTimeString();

    }

    // Update is called once per frame
    void Update()
    {
        scoreInGame.text = GameManager.Instance.Score.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SaveSystem.SaveScore(120);
            var timer = Timer.GetInstance();
            timer.SetTextMesh(timeInGame);
            timer.StartTimer();

            spawnerTunnel.SetActive(true);
            startTxt.SetActive(false);

            playerRB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;

        }

        playerRB.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        if (!playerIsAlive) {
            return;
        } 

        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y <= maxHeightThreshold)
        {
            AudioManager.current.PlaySound(musicFx);
            playerRB.AddForce(Vector3.up * force, forceMode);
        }

        if (GameManager.Instance.CurrentScene == 3 && GameManager.Instance.Score == 10)
        {
            var timer = Timer.GetInstance();
            timer.StopTimer();
            Debug.Log("fin del juego -> " + timer.BestTime);
            Debug.Log(SaveSystem.LoadScore());

            Debug.Log(timer.BestTime);
            time.text = timer.GetTimeString();
            bestTime.text = timer.BestTime;

            endScreen.SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        if (spawnerTunnel.activeSelf)
        {
            birdMode.rotation = playerRB.velocity.y > 0
               ? Quaternion.Lerp(birdMode.rotation, Quaternion.Euler(0, 0, 0), 0.5f)
               : Quaternion.Lerp(birdMode.rotation, Quaternion.Euler(0, 0, -30), 0.1f);
        }
    
    }


    private void OnPlayerDeath()
    {
        playerIsAlive = false;
    }
}
