using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawningMobile : MonoBehaviour
{
    [SerializeField] private GameManager gmScript;
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;
    
    private bool _paused;
    public int fileOnScreen;
    private Vector2 spawnZone;
    private float timer;
    private float timeToSpawn;
    private int counter;
    private bool _isFinished = false;
    public int score = 0;
    private int random;
    private int nbreofspawn;
    [SerializeField] private List<GameObject> allPaper = new List<GameObject>();
    public List<GameObject> spawnOnScreen = new List<GameObject>();
    private string[] feuilletag = { "Red", "Blue", "Green" };
    [SerializeField] private AudioSource winAudio;
    [SerializeField] private AudioSource loseAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Starting();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _paused = !_paused;
            if (_paused) Pause();
            else Resume();
        }
        
        if (!_isFinished)
        {
            if (timeToSpawn <= timer)
            {
                timeToSpawn += Time.deltaTime;
            }
            else
            {
                timeToSpawn = 0f;
                ToSpawn();
                nbreofspawn++;
                fileOnScreen = 0;
                CountOnScreen(fileOnScreen);
                counter++;
            }

            if (counter == 5)
            {
                counter = 0;
                if (timer - gmScript.hard >= 0.2f) timer = timer - gmScript.hard;
                
            }
            
            
            if (nbreofspawn >= gmScript.maxfile)
            {
                winAudio.Play();
                _isFinished = true;
                _scoreText.text = "Score: " + score;
                gmScript.creditsCanvas.gameObject.SetActive(true);
                WinPanel.SetActive(true);
            }

            if (fileOnScreen >= 28)
            {
                loseAudio.Play();
                _isFinished = true;
                _scoreText.text = "Score: " + score;
                gmScript.creditsCanvas.gameObject.SetActive(true);
                LosePanel.SetActive(true);
                
            }

            if (_isFinished)
            {
                foreach (GameObject spawnedObject in spawnOnScreen)
                {
                    Destroy(spawnedObject);
                }
                _scoreText.gameObject.SetActive(true);
                gmScript.gameObject.SetActive(true);
            }

           
        }

        
    }

    

    public void Pause()
    {
        Time.timeScale = 0;
        gmScript.pauseCanvas.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gmScript.pauseCanvas.gameObject.SetActive(false);
    }
    
    private void ToSpawn()
    {
        spawnZone = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 5f));
        random = Random.Range(0, gmScript.difficulty);
        GameObject newFile = (GameObject) Instantiate(allPaper[random], spawnZone, Quaternion.identity);
        spawnOnScreen.Add(newFile);
    }

    private void Restart()
    {
        _scoreText.gameObject.SetActive(false);
    }

    private int CountOnScreen(int a)
    {
        int total= 0;
        foreach (GameObject feuille in spawnOnScreen)
        {
            if (feuille != null)
            {
                fileOnScreen++;
            }
        }

        return total;
    }

    public void Starting()
    {
        fileOnScreen = 0;
        _isFinished = false;
        timer = 3f;
        timeToSpawn = 0;
        counter = 0;
        _isFinished = false;
        score = 0;
        nbreofspawn = 0;
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);
    }
}
