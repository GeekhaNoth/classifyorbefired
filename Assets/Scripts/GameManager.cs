using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Application = UnityEngine.Application;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas difficultyCanvas;
    public Canvas creditsCanvas;
    [SerializeField] private Canvas settingCanvas;
    [SerializeField] private GameObject allDocuments;
    public Canvas pauseCanvas;
    [SerializeField] private Spawning spawning;
    public int difficulty = 3;
    public float hard;

    public int maxfile;
    
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider sfxSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCanvas.gameObject.SetActive(true);
        pauseCanvas.gameObject.SetActive(false);
        difficultyCanvas.gameObject.SetActive(false);
        settingCanvas.gameObject.SetActive(false);
        allDocuments.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && difficultyCanvas.gameObject.activeSelf)
        {
            difficultyCanvas.gameObject.SetActive(false);
            mainCanvas.gameObject.SetActive(true);
        }
    }
    
    

    public void GoToDifficulty()
    {
        mainCanvas.gameObject.SetActive(false);
        allDocuments.gameObject.SetActive(false);
        difficultyCanvas.gameObject.SetActive(true);
    }

    public void Settings()
    {
        mainCanvas.gameObject.SetActive(false);
        settingCanvas.gameObject.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }


    public void EasyMode()
    {
        difficulty = 3;
        hard = 0.5f;
        maxfile = 50;
        Launch();
    }
    public void MediumMode()
    {
        difficulty = 9;
        hard = 0.60f;
        maxfile = 100;
        Launch();
    }

    public void HardMode()
    {
        difficulty = 9;
        hard = 0.75f;
        maxfile = 150;
        Launch();
    }

    public void Launch()
    {
        Start();
        mainCanvas.gameObject.SetActive(false);
        spawning.Starting();
        allDocuments.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        mainCanvas.gameObject.SetActive(true);
        settingCanvas.gameObject.SetActive(false);
        allDocuments.gameObject.SetActive(false);
    }

    public void PlaySound(AudioSource clip)
    {
        clip = clip.GetComponent<AudioSource>();
        clip.Play();
    }
    
    public void ChangeSfxVolume(float value)
    {
        mixer.SetFloat("Master", Mathf.Log10(sfxSlider.value) * 20);
    }
}