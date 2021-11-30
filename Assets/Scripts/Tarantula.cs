using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarantula : MonoBehaviour
{    
    public GameObject player;
    public bool woke; 
    void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if(woke){
            gameObject.SetActive(true);
            transform.position = Vector2.MoveTowards(transform.position,player.transform.position, 3f * Time.deltaTime);
        }
    }
    public void WakeUp(){
        woke = true;
    }
}
