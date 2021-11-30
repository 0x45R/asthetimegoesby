using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class FollowLight : MonoBehaviour
{
    public int wait = 0;

    public bool started = false;

    public GameObject text;

    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private float moveSpeed = 2f;

    private int waypointIndex = 0;

    IEnumerator Blind()
    {
        text.GetComponent<WriteText>().gamestarted = false;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene((4).ToString());
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(wait);
        started = true;
    }

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        StartCoroutine(WaitForStart());
    }

    private void Update()
    {
        started = text.GetComponent<WriteText>().waited;

        if (started)
        {
            if (waypointIndex <= waypoints.Length - 1)
            {
                transform.position =
                    Vector2
                        .MoveTowards(transform.position,
                        waypoints[waypointIndex].transform.position,
                        moveSpeed * Time.deltaTime);

                if (
                    transform.position ==
                    waypoints[waypointIndex].transform.position
                )
                {
                    waypointIndex += 1;
                }
            }
            else
            {
                if (
                    gameObject.GetComponent<Light2D>().pointLightOuterRadius <
                    1001
                )
                {
                    gameObject.GetComponent<Light2D>().pointLightOuterRadius +=
                        5;
                }
                else
                {
                    StartCoroutine(Blind());
                    started = false;
                }

                gameObject.GetComponent<Light2D>().color =
                    new Color(255, 255, 255);
                gameObject.GetComponent<Light2D>().intensity = 1000;
            }
        }
    }
}
