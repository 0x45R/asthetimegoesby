using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Button button;
    public GameObject settingsMenu;

    void Start(){
        button.onClick.AddListener(OnClick);
    }
    void OnClick(){
        StartCoroutine(runAnimations());
    }
    IEnumerator runAnimations(){
        if(!settingsMenu.gameObject.activeSelf){
            settingsMenu.gameObject.SetActive(true);
            settingsMenu.GetComponent<Animation>().Play("FadeIn");
        }else{
            settingsMenu.GetComponent<Animation>().Play("FadeOut");
            yield return new WaitForSeconds(0.25f);
            settingsMenu.gameObject.SetActive(false);
        }
        
    }
}
