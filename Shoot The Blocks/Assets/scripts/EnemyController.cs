using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    [SerializeField]
    private UnityEngine.Rendering.Universal.Light2D light;

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
        target = GameObject.FindGameObjectWithTag("Center_Player").GetComponent<Transform>();
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        bool futureCollisionDetected = false;
        Transform collisionPosition = target;

        foreach (GameObject enemy in enemies){
            if(!enemy.Equals(this.gameObject)){
            float range = Vector2.Distance(transform.position, enemy.transform.position);
            if(range < 1.5f){
                //Debug.Log("Distance between: " + range);
                collisionPosition = enemy.transform;
                futureCollisionDetected = true;
            }
            
            }
            
        }

        if(!futureCollisionDetected && target != null){
            Vector2 direction = target.transform.position - transform.position;

            direction.Normalize();

            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;

            transform.position = Vector2.MoveTowards(transform.position, target.position, Random.Range(0.2f,moveSpeed) * Time.deltaTime/2);
            
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else if(target != null){

            transform.position = Vector2.MoveTowards(transform.position, collisionPosition.position, -2.5f * Random.Range(0.2f,moveSpeed) * Time.deltaTime/2);

        }

        
        
        if (Time.time > lightOn)
        {
            light.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.tag != "Player")
        {
            if(collision.tag == "bullet"){
                gameManager.SendMessage("givePoints", 50);
            }
            Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
