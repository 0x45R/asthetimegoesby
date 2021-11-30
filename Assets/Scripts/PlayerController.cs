using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D controller;
    public float moveSpeed;
    public float jumpForce;
    public Vector2 maxSpeed;
    public WriteText WriteText;
    public Animation animation;
    public Tarantula tarantula;
    
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadScene());
        }
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (WriteText.gamestarted)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            float forceX = inputX * moveSpeed * Time.deltaTime;
            float forceY = inputY * jumpForce * Time.deltaTime;

            Vector2 force = new Vector2(forceX, forceY);

            controller.AddForce(force);
            if (controller.velocity.y > maxSpeed.y)
            {
                controller.velocity = new Vector2(controller.velocity.x, maxSpeed.y);
            }
            if (controller.velocity.x > maxSpeed.x)
            {
                controller.velocity = new Vector2(maxSpeed.x, controller.velocity.y);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        string level = other.gameObject.name;
        if(level.Contains("Tarantula")){
            if(level.Contains("WakeUp")){
                StartCoroutine(TarantulaPanic());
                tarantula.gameObject.SetActive(true);
                tarantula.SendMessage("WakeUp");
            }
            else{
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (level.Contains("Portal"))
        {
            level = level.Remove(0, 6);
            PlayerPrefs.SetString("LevelState", (int.Parse(level) - 1).ToString());
            Loader.Load(level);
        }
        if (level.Contains("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    IEnumerator TarantulaPanic(){
        WriteText.gamestarted = false;
        yield return new WaitForSeconds(1f);
        WriteText.gamestarted = true;
    }
    IEnumerator LoadScene(){
        animation.Play("FadeIn");
        yield return new WaitForSeconds(.5f);
        Loader.Load("LevelSelector");
    }
    public void GotoSleep(){
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
