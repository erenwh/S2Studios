using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

    public float splashTime = 2.0f;         //how long will the splash be shown
    private float viewingTime = 0.0f;       //how long the scene has been viewed
	
	// Update is called once per frame
	void Update () {
        viewingTime += Time.deltaTime;
        if (viewingTime >= splashTime) {
            SceneManager.LoadScene("Menu");
        }
	}
}
