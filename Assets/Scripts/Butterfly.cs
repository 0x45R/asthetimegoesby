using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Butterfly : MonoBehaviour
{
    public int bullets;

    public GameObject bullet;

    public Animation animation;

    public bool woke;

    public Rigidbody2D controller;

    public float moveSpeed;

    public float jumpForce;

    public Vector2 maxSpeed;

    public cameraFollow camera;

    public Transform target;

    public bool canShoot = true;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (woke)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            float forceX = inputX * moveSpeed * Time.deltaTime;
            float forceY = inputY * jumpForce * Time.deltaTime;

            Vector2 force = new Vector2(forceX, forceY);

            controller.AddForce (force);
            if (controller.velocity.y > maxSpeed.y)
            {
                controller.velocity =
                    new Vector2(controller.velocity.x, maxSpeed.y);
            }
            if (controller.velocity.x > maxSpeed.x)
            {
                controller.velocity =
                    new Vector2(maxSpeed.x, controller.velocity.y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadScene());
        }
        if (Input.GetKey("space"))
        {
            if (canShoot)
            {
                StartCoroutine(SpawnBullet());
            }
        }
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (woke)
        {
            gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnBullet()
    {
        canShoot = false;
        GameObject b = Instantiate(bullet);
        b.transform.position =
            Vector2
                .MoveTowards(gameObject.transform.position,
                target.position,
                1.0f);
        bullets += 1;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string level = other.gameObject.name;
        if (level.Contains("Fist"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (level.Contains("Portal"))
        {
            level = level.Remove(0, 6);
            PlayerPrefs
                .SetString("LevelState", (int.Parse(level) - 1).ToString());
            Loader.Load (level);
        }
        if (level.Contains("Jeremy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (level.Contains("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void WakeUp()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        animation.Play("FadeIn");
        yield return new WaitForSeconds(0.5f);
        woke = true;
        camera.target = gameObject.transform;
        camera.offset = new Vector3(0, 0, -10);
        camera.gameObject.GetComponent<Camera>().orthographicSize = 10.0f;
        yield return new WaitForSeconds(1.0f);
        animation.Play("FadeOut");
    }

    IEnumerator LoadScene()
    {
        animation.Play("FadeIn");
        yield return new WaitForSeconds(.5f);
        Loader.Load("LevelSelector");
    }
}
