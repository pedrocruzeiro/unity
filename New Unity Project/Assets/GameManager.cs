using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 screenBounds;

    [SerializeField]
    private GameObject enemy;

    private float spawnTime = 2.0f;

    private float nextSpawn = 0.0f;

    private float points = 0.0f;

    [SerializeField]
    private SpriteRenderer background;


    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private float timeLeft;

    private Color targetColor;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        scoreText.SetText("Points: " + points);
        targetColor = background.color;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("timeLeft: " + timeLeft);
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
        

        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnTime;
            Vector2 spawnPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.value < 0.5f ? screenBounds.y : -screenBounds.y);
            Instantiate(enemy, spawnPosition, Quaternion.identity);
                }
    }
     
    public void givePoints(float pointsValue)
    {
        points = points + pointsValue;
        scoreText.SetText("Points: " + points);
    }
   
}
