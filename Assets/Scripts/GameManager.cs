using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

    public static bool gameRunning = false;

    public static DateTime gameStarted;
    
    public GameObject gshBall;
    
    float min = 1f;
    float max = 2f;
    
    [Header("Score Elements")] 
    private TimeSpan timePlayed;
    public Text InfoText;
    public Text highScoreText;
    
    [Header("Audio & Sounds")] 
    public AudioClip failedSound;
    private AudioSource audioSource;

    [Header("Game Over")] 
    public GameObject gameOverPanel;
    public Text GameOverPanelScoreText;

    private float highScore;

    private void checkScore()
    {
        Debug.Log(timePlayed.TotalMilliseconds);
        Debug.Log(TimeSpan.FromMilliseconds(timePlayed.TotalMilliseconds).ToString());
        if (highScore < (float)timePlayed.TotalMilliseconds) {
            highScore = (float)timePlayed.TotalMilliseconds;
            PlayerPrefs.SetFloat("highscore", (float)timePlayed.TotalMilliseconds);
            setHighscoreText();
        }
    }

    private void setHighscoreText()
    {
        highScoreText.text = "Best: " + TimeSpan.FromMilliseconds(highScore);
    }
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        highScore = PlayerPrefs.GetFloat("highscore");
        setHighscoreText();
    }
    
    private void Start()
    {
        StartCoroutine(SpawnFruits());
    }

    public void stopGame()
    {
        //audioSource.PlayOneShot(failedSound);
        gameRunning = false;
        gameOverPanel.SetActive(true);
        GameOverPanelScoreText.text = "Time: " + TimeSpan.FromMilliseconds(timePlayed.TotalMilliseconds);
        checkScore();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactable")) {
            Destroy(g);
        }
    }
    
    private void Update()
    {
        if (!gameRunning) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                startRound();
            }
        }

        if (gameRunning) {
            timePlayed = DateTime.Now - gameStarted;
            InfoText.text = "Time: " + TimeSpan.FromMilliseconds(timePlayed.TotalMilliseconds);
        }
    }

    
    
    private void startRound()
    {
        if (gameRunning) {
            return;
        }
        gameStarted = DateTime.Now;
        gameRunning = true;
        gameOverPanel.SetActive(false);
        GameOverPanelScoreText.text = "";
    }
    
    private IEnumerator SpawnFruits()
    {
        while (true) {
            float hardMaker = (timePlayed.Seconds + (timePlayed.Minutes*60))/10 + 1;
            yield return new WaitForSeconds(Random.Range(min/hardMaker, max/hardMaker));
            if (gameRunning) {
                Transform t = new GameObject().transform;
    
                float x = Random.Range(-8f, 8f);
                t.SetPositionAndRotation(new Vector3(x,8,0),Quaternion.Euler(0,0,0));
                GameObject fruit = Instantiate(gshBall, t);
    
                Destroy(fruit, 5);
            }
        }
    }
}
