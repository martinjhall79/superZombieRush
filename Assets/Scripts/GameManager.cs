/// <summary>
/// Manages game state
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Text continueText;
    public Text scoreText;

    // Pause game when player killed
    private TimeManager timeManager;
    // Restart the game after player killed
    private bool gameStarted;
    private GameObject player;
    private GameObject floor;
    private GameObject spawner;
    private float timeElapsed = 0f;
    private float bestTime = 0f;

    // Blinking effect on text
    private bool blink;
    private float blinkTime = 0f;

    // Highlight best time text with different colour
    private bool beatBestTime;

    private void Awake()
    {
        floor = GameObject.Find("Foreground");
        spawner = GameObject.Find("Spawner");
        timeManager = GetComponent<TimeManager>();
    }

    // Use this for initialization
    void Start()
    {
        // Realign floor
        var floorHeight = floor.transform.localScale.y;
        // Position vector to manipulate, once we've calculated where floor needs to go on the screen
        var pos = floor.transform.position;
        pos.x = 0;
        // Calculate where screen bottom is, and offset height of floor from screen height
        pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2) + (floorHeight / 2);

        spawner.SetActive(false);
        
        // Pause game until player presses key to start, Update listens for key press
        Time.timeScale = 0;

        continueText.text = "PRESS ANY BUTTON TO START";

        // Load the high score
        bestTime = PlayerPrefs.GetFloat("BestTime");
    }

    // Update is called once per frame
    void Update()
    {
        // When player killed, listen for button press to restart the game
        if (!gameStarted && Time.timeScale == 0) {
            if (Input.anyKeyDown) {
                // Reset timescale (game pause effect) 
                timeManager.ManipulateTime(1, 1f);
                // Restart game
                ResetGame();
            }
        }

        // Manually increment time by 1 every frame to blink text, independent of timescale manipulation functions
        if (!gameStarted) {
            blinkTime++;

            if (blinkTime % 40 == 0) {
                blink = !blink;
            }

            continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);

            // Highlight new best time text in yellow
            var textColour = beatBestTime ? "#FF0" : "FFF";

            // Show time and best time when game not running
            scoreText.text = "TIME: " + FormatTime(timeElapsed) + "\n<color=" + textColour + ">BEST: " + FormatTime(bestTime) + "</color>";
        } else { // Just display elapsed time of this game
            timeElapsed += Time.deltaTime;
            scoreText.text = "TIME: " + FormatTime(timeElapsed);
        }
    }

    // End the game when player pushed off screen
    // When this method is called, deactivate spawner
    void OnPlayerKilled()
    {
        spawner.SetActive(false);
        // Remove delegate linkage to ensure object gets destroyed, to avoid memory leaks
        var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
        playerDestroyScript.DestroyCallback -= OnPlayerKilled;

        // Reset player velocity
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Manipulate time down to zero over a few seconds
        timeManager.ManipulateTime(0, 5.5f);
        gameStarted = false;

        continueText.text = "PRESS ANY BUTTON TO RESTART";

        // Save the high score
        if (timeElapsed > bestTime) {
            bestTime = timeElapsed;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            beatBestTime = true;
        }
    }

    void ResetGame()
    {
        Debug.Log("Reset game");
        spawner.SetActive(true);

        // Respawn player
        player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0, (Screen.height/PixelPerfectCamera.pixelsToUnits) / 2 + 100, 0));

        // Delegate allows scripts to communicate without having to know many details about the linkage between them
        var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
        // Connect up delegate by pointing property to OnPlayerKilled method
        // Tell DestroyOffScreen to call OnPlayerKilled in this script when DestroyCallback gets called
        playerDestroyScript.DestroyCallback += OnPlayerKilled;

        // Restart the game
        gameStarted = true;

        // Hide restart text
        continueText.canvasRenderer.SetAlpha(0);

        // Reset time elapsed
        timeElapsed = 0;
        beatBestTime = false;
    }

    // Format time for scoring
    string FormatTime(float value)
    {
        TimeSpan t = TimeSpan.FromSeconds(value);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }
 }
