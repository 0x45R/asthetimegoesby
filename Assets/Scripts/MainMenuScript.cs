using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public Animation animation;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene(){
        animation.Play("FadeIn");
        yield return new WaitForSeconds(.5f);
        Loader.Load("LevelSelector");
    }
}
