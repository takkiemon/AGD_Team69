using GameObjectControllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class LudoGameMasterController : MonoBehaviour
{
    public int _currentLevel;
    private bool _gameOver;

    public GameObject Player;
    public GameObject MainCamera;
    public GameObject BlankCamera;
    public GameObject PausedTintPlane;
    public GameObject PlayingUi;
    public GameObject PausedUi;
    public GameObject GameOverUi;
    public LudoLevelGenerator ludoLevelGen;
    public Text GameOverLevel;
    public Text Level;

    // Start is called before the first frame update
    void Start()
    {
        AnalyticsEvent.GameStart();
        // Set scene to dark
        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.ambientLight = Color.black;

        // Generate the first level
        SetViewBlank();
        _currentLevel = 1;
        _gameOver = false;

        // Start the game
        SetViewNormal();
        Level.text = "Level: " + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameOver)
        {
            // Restart the game if game is over and r pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        // Check for pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale.Equals(1.0f))
            {
                Pause();
                return;
            }
            else
            {
                UnPause();
            }
        }

        // Check for game over
        if (Player.GetComponent<PlayerController>().Dead)
        {
            AnalyticsEvent.LevelFail("level_" + _currentLevel);
            GameOver();
        }
    }

    // For turning the screen black during level generation
    private void SetViewBlank()
    {
        MainCamera.SetActive(false);
        BlankCamera.SetActive(true);
    }

    private void SetViewNormal()
    {
        BlankCamera.SetActive(false);
        MainCamera.SetActive(true);
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        PausedTintPlane.SetActive(true);
        PlayingUi.SetActive(false);
        PausedUi.SetActive(true);
    }

    private void UnPause()
    {
        PausedUi.SetActive(false);
        PlayingUi.SetActive(true);
        PausedTintPlane.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void GameOver()
    {
        PausedTintPlane.SetActive(true);
        PlayingUi.SetActive(false);
        GameOverUi.SetActive(true);
        GameOverLevel.text = "Reached level " + _currentLevel + "!";
        _gameOver = true;
    }

    public void LevelUp()
    {
        AnalyticsEvent.LevelComplete("level_" + _currentLevel);
        _currentLevel++;
        if (ludoLevelGen == null)
        {
            ludoLevelGen = FindObjectOfType<LudoLevelGenerator>();
            Debug.Log("test 001 ludocontroller");
        }
        Destroy(ludoLevelGen.levelParent);
        ludoLevelGen.InitLudoLevelGen();
        Debug.Log("test 002 ludocontroller");
        
        //DontDestroyOnLoad(Player);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
