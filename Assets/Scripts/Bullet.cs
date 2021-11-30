using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    private int health = 10;

    public Transform target;

    public float moveSpeed;

    public bool isClone;

    void Start()
    {
        isClone = gameObject.name.Contains("(Clone)");
    }

    void Update()
    {
        if (isClone)
        {
            Vector3 vectorToTarget = target.position - transform.position;
            float angle =
                Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation =
                Quaternion
                    .Slerp(transform.rotation, q, Time.deltaTime * moveSpeed);
            transform.position =
                Vector2
                    .MoveTowards(transform.position,
                    target.position,
                    moveSpeed * Time.deltaTime);
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.position =
                transform.parent.transform.position - new Vector3(0, -100, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            if (isClone)
            {
                health -= 1;
            }
        }
        if (collision.gameObject.name == "Jeremy")
        {
            if (isClone)
            {
                collision.gameObject.SendMessage("Hurt");
                Destroy(this.gameObject);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
