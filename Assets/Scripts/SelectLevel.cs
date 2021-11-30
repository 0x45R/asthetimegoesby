using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    private int unlockedLevels;
    void Start()
    {   
        if(PlayerPrefs.HasKey("LevelState")){
            unlockedLevels = int.Parse(PlayerPrefs.GetString("LevelState"));
            Debug.Log(unlockedLevels);
        }
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        if(!(unlockedLevels+1 >= int.Parse(gameObject.transform.name))){
            gameObject.GetComponent<Image>().color = new Color32(200,200,200,255);
        }
    }
    void OnClick()
    {
        Debug.Log(gameObject.transform.name);
        Debug.Log(unlockedLevels);
        if(unlockedLevels+1 >= int.Parse(gameObject.transform.name)){
            Loader.Load(gameObject.transform.name);
        }

    }
}
