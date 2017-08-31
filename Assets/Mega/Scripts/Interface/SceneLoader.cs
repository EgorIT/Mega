using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void Click () {
        //Debug.Log("Click");
        SceneManager.LoadSceneAsync("11_TEST");
    }

    void Update () {
        if(Input.GetKeyDown("l")) {
            //Cli
        }
    }
}
