using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class Jeremy : MonoBehaviour
{
    public float maxHealth;

    public AudioSource audioSource;

    public GameObject bullet;

    public float health;

    public Slider bossBar;

    public WriteText textMesh;

    public float radiusOffset;

    public Transform target;

    public AudioClip[] audioClips = new AudioClip[] { };

    public string[]
        bossMessages =
            new string[] {
                "Muhahaaha!",
                "You'll never defeat me!",
                "DIE!",
                "I've had ENOUGH!",
                "GIVE UP!"
            };

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bossBar.gameObject.SetActive(false);
        StartCoroutine(Attack());
    }

    public void Update()
    {
        if (textMesh.waited)
        {
            bossBar.gameObject.SetActive(true);
            transform.position =
                Vector2
                    .MoveTowards(transform.position,
                    target.position,
                    1.0f * Time.deltaTime);
        }
    }

    public void Hurt()
    {
        health -= 5f;
        UpdateBossBar();
    }

    IEnumerator Death()
    {
        bossBar.gameObject.SetActive(false);
        PlayerPrefs.SetString("LevelState", "9");
        Loader.Load("9");
        yield return new WaitForSeconds(1);
    }

    void UpdateBossBar()
    {
        gameObject.transform.localScale =
            new Vector2(((health / maxHealth) * 5) + radiusOffset,
                ((health / maxHealth) * 5) + radiusOffset);
        if (health <= 0)
        {
            StartCoroutine(Death());
        }
        bossBar.value = health / maxHealth;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(3);
        if (textMesh.waited)
        {
            GameObject b = Instantiate(bullet);
            b.transform.position =
                Vector2
                    .MoveTowards(gameObject.transform.position,
                    target.position,
                    1.0f);
            var randomnum = Random.Range(0, bossMessages.Length);
            textMesh.gameObject.GetComponent<TMP_Text>().text =
                bossMessages[randomnum];
            audioSource.clip = audioClips[randomnum];
            audioSource.Play();
            yield return new WaitForSeconds(Random.Range(3, 5));

            textMesh.gameObject.GetComponent<TMP_Text>().text = "";
        }
        StartCoroutine(Attack());
    }
}
