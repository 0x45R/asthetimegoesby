using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public static void Load(string scene){
        SceneManager.LoadScene(scene);
    }

}
