using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private float moveSpeed = 100f;

    private float movement = 0f;

    private bool shoot = false;

    private bool gameOver = false;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float _bulletSpawnTime = 1F;

    // Update is called once per frame
    void Update()
    {

        movement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (!gameOver)
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.deltaTime * -moveSpeed);
        }

    }

    private void FixedUpdate()
    {

        
        
    }

    private void Shoot()
    {
        GameObject bulletShot = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D bulletRb = bulletShot.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.position * 5f;
        bulletRb.AddRelativeForce(Vector2.up * 5f);
        Destroy(bulletShot, _bulletSpawnTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Enemy")
        {
            gameOver = true;
            SceneManager.LoadScene(0);
        }
    }
}
