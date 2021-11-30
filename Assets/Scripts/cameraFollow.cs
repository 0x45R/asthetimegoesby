using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public GameObject fadein;
    public float delay;

    void Start(){
        fadein.GetComponent<Animation>().Play("FadeOut");
    }
    void FixedUpdate()
    {
        transform.position=target.position+offset;

    }
    IEnumerator moveTo(){
        yield return new WaitForSeconds(delay);
        transform.position=target.position+offset;
    }
}
