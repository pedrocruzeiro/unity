using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    [SerializeField]
    private Light2D light;

    private GameObject gameManager;

    private float lightOn = 0.2f;

    [SerializeField]
    private ParticleSystem explosion;

    private float currentTime;

    private float moveSpeed;

    private float maxSpeed = 4f;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        light.enabled = false;
        lightOn = Time.time + lightOn;
        moveSpeed = Time.timeSinceLevelLoad / 60;
        if(moveSpeed > maxSpeed)
        {
            moveSpeed = maxSpeed;
        }
        //Debug.Log("speed :" + moveSpeed);
     }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, target.position, Random.Range(0.2f,moveSpeed) * Time.deltaTime);
        if (Time.time > lightOn)
        {
            light.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            gameManager.SendMessage("givePoints", 50);
            Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
