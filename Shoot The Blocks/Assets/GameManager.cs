using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 screenBounds;

    [SerializeField]
    private GameObject enemy;

    private float changeLevelTime = 30.0f;

    private float spawnTime = 2.0f;

    private float nextSpawn = 0.0f;

    public float points = 0.0f;

    public float gameTime = 0.0f;

    [SerializeField]
    private SpriteRenderer background;


    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI level;
    
    [SerializeField]
    private int levelNumber;

    [SerializeField]
    private float timeLeft;

    public bool gameIsPaused = false;

    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private GameObject startMenu;

   [SerializeField]
    private GameObject HighScoreUI;

    [SerializeField]
    private int maxEnemies;

    private Color targetColor;

    private bool gameStarted = false;

    private bool gameOver = false;

    public GameObject gameOverUI;

    public UnityEvent<float> submitGameOverPointsEvent;
    
    void Start()
    {
        Debug.Log("Game Started!!");
        levelNumber = 1;
        gameOverUI.SetActive(false);
        startMenu.SetActive(true);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        scoreText.SetText("Points: " + points);
        level.SetText("Level " + levelNumber);
        Time.timeScale = 0;
        targetColor = background.color;
    }

    // Update is called once per frame
    void Update()
    {


        gameTime += Time.deltaTime;

        if(Input.anyKey && !gameStarted)
        {
            gameStarted = true;
            Time.timeScale = 1;
            startMenu.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            if(gameIsPaused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
            
        }

            if (timeLeft <= Time.deltaTime)
            {
                // transition complete
                // assign the target color
                background.color = targetColor;

                // start a new transition
                targetColor = new Color(Random.value, Random.value, Random.value);
                timeLeft = 5.0f;
            }
            else
            {
                // transition in progress
                // calculate interpolated color
                background.color = Color.Lerp(background.color, targetColor, Time.deltaTime / timeLeft);

                // update the timer
                timeLeft -= Time.deltaTime;
            }
            //Color targetColor = new Color(Random.value, Random.value, Random.value);
            //background.color = Color.Lerp(background.color, targetColor, Time.deltaTime / 1.0f);


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (gameTime > nextSpawn && enemies.Length < maxEnemies ) {
            nextSpawn = Time.time + spawnTime;
            Vector2 spawnPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.value < 0.5f ? screenBounds.y : -screenBounds.y);
            Instantiate(enemy, spawnPosition, Quaternion.identity);
                }

        if (gameTime > changeLevelTime){
            Debug.Log("Game update level!! : " + changeLevelTime + " - " + gameTime);
            changeLevelTime = gameTime + changeLevelTime;
            maxEnemies++;
            levelNumber++;
            spawnTime = spawnTime - 0.25f;
            level.SetText("Level " + levelNumber);
        }
        
        
    }
     
    public void givePoints(float pointsValue)
    {
        points = points + pointsValue;
        scoreText.SetText("Points: " + points);
    }


    void PauseGame ()
    {
        Debug.Log("Pause Log");
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame ()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadScene(string sceneName)
    {
        ResumeGame();
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
            Application.Quit();
        
    }


    public void gameOverEvent(){
        gameOver = true;
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
        HighScoreUI.SetActive(true);
        Debug.Log("Game manager points are: " + points);
        submitGameOverPointsEvent.Invoke(points);
    }
   
}
