using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public enum GameState {none, menu, goalReached, playing, oops, gameOver}
public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager S;
    // Gamestate
    public GameState currentState = GameState.none;

    // UI elements
    //public Text gameMessageText;
    //public Text gameNameText;
    public Text scoreText;
    public Text timeText;

    // Game parameters
    //public GameObject playerPrefab;
    //private GameObject currentPlayer;

    private bool gameStarted = false;
    private int currentLevel = 0;
    private int currentTransition;
    private int secondsRemaining;

    CustomSceneManager sceneLoader; // Reference to the SceneLoader script
    

    private int score = 0;
    //private int numberOfPlayerDeaths; // times player has died 
    private float timeRemaining;
    private float TIME_AT_START = 0.0f; // change to be longer
    
    private void Awake()
    {
        // Ensure GameManager is a singleton
        if (S == null)
        {
            S = this;
            DontDestroyOnLoad(this.gameObject); // Persist across scene loads
        }
        else
        {
            Destroy(this.gameObject); // Destroy any duplicate GameManager instances
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<CustomSceneManager>();

        // Go to game menu
        SetGameMenu();
 
    }

    private void SetGameMenu(){

        // Set the state to menu
        currentState = GameState.menu;
        InitializeGame();
        Debug.Log("game initialized");

    }

    // Update is called once per frame
    void Update()
    {

        // Check if the game has started and load level 1 if it hasn't
        // if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        // {
            
        // }
        
    }

    private void InitializeGame(){


        // Reset the score & text value
        score = 0;
        scoreText.text = "000";
        
        // Reset level to level 1 
        currentTransition = 0;

        // Reset the lives
        //numberOfPlayerDeaths = 0;
        

        // Reset the time
        secondsRemaining = 0;
        timeRemaining = TIME_AT_START;
        gameStarted = true;

        // Go to the get start state
        StartRound();

    }

    private void StartRound(){
        
        // Play game background sound
        Debug.Log("sound playing");
        SoundManager.S.PlayGameSound();

        // Put us into playing start
        currentState = GameState.playing;

        // Reset time
        timeRemaining = 0;

        // Start game countdown
        StartCoroutine(GameCountUp()); // take this out        

    }

    // public IEnumerator OopsState(){

    //     // Set the message
    //     // gameMessageText.enabled = true;
    //     // gameMessageText.text = "You have " + livesRemaining + " lives left!";

    //     yield return new WaitForSeconds(2.0f);

    //            // Decide whether the game is over or not
    //     // if (livesRemaining <= 0){
            
    //     //     // the game is over
    //     //     GameOver();

    //     // } else {
    //     //     // Reset the game
    //     StartRound();
    //     // }
    //     Debug.Log("Running oops start");

    // }

    private void GameOver(){
        // Change the gamestate
        currentState = GameState.gameOver;
        
        InitializeGame();

    }

    // public void PlayerOutOfPlay(){

    //     // This is called when a raccoon gets hit by an obstacle
        
    //     // Stop sound
    //     SoundManager.S.StopSound();

    //     // Go to the oops state
    //     currentState = GameState.oops;

    //     // numberOfPlayerDeaths++; // change to times died

    //     StartCoroutine(OopsState());


    // }

    public void GoalReached(){// when player reaches the last sun
        
        currentState = GameState.goalReached;

        SoundManager.S.StopSound();

        GameOver();
    }

    public void PlayerScore(int Playerscore){
        // Update score
        score += Playerscore;
        scoreText.text = score.ToString();

    }


    IEnumerator GameCountUp(){
        while (currentState == GameState.playing)
        {
            // Update your variable every second
            secondsRemaining = Mathf.FloorToInt(timeRemaining);

            timeText.text = secondsRemaining.ToString();
            // Wait for one second
            yield return new WaitForSeconds(1.0f);

            // increase the countdown time by one second
            timeRemaining +=  1.0f;

            // if (timeRemaining == 0.0f){
            //     PlayerOutOfPlay();
            // }
        }
    }

    // public IEnumerator PlayAgain(){ // rework this

    //     yield return new WaitForSeconds(2.0f);

    //     // Display score
    //     //gameMessageText.text = "You Scored: " + score;

    //     yield return new WaitForSeconds(4.0f);
    //     //gameMessageText.text = "Press \"Space\" to Play Again";

    //     // Change game state
    //     currentState = GameState.menu;

    //     yield return new WaitForSeconds(5.0f);
    //     //gameMessageText.text = "Thanks for playing!";
        
    //     if(currentState == GameState.menu){
    //         SetGameMenu();
    //         // go to beginning
    //     }
        
    // }

    //     // Load next level
    // public void LoadNextLevel()
    // {
    //     currentLevel++;
    //     //SceneManager.LoadScene("Level" + currentLevel);
    // }

    //     public void LoadNextTransition()
    // {
    //     currentTransition++;
    //     //SceneManager.LoadScene("Transition" + currentTransition);
    // }

    // // Restart the game
    // public void RestartGame()
    // {
    //     score = 0;
    //     currentLevel = 1;
    //     //SceneManager.LoadScene("Level1");
    // }
}


