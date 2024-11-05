using System.Collections;
using UnityEngine;
using TMPro; //Newer version
using UnityEngine.SceneManagement;

public class EvacuationTime : MonoBehaviour {
    public float timeLeft = 10.0f;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI staticText;
    public TextMeshProUGUI goodEnd;

    void Start() {
        //Manually changing the text
        if (staticText != null) {
            staticText.text = "Time:"; 
        }

        //Want our goodEnd to not happen in the beginning but at the end
        //so turned off for now
        if(goodEnd != null) {
            goodEnd.gameObject.SetActive(false);
        }

    }
    
    //NOTE: Used once every frame
    void Update() {
        // Decreases timer
        timeLeft -= Time.deltaTime; 

        // Will show time left in our UI
        startText.text = Mathf.Ceil(timeLeft).ToString();

        //Once timer runs out goodEnd will occur and game restarted after
        //15 second delay
        if (timeLeft <= 0 && !goodEnd.gameObject.activeSelf) {   
            goodEnd.gameObject.SetActive(true); 
            goodEnd.text = "Evacuation Complete! Restarting Game in 15s.";
            timeLeft = 0;
            StartCoroutine(restartGame());
        }
    }

    IEnumerator restartGame() {
        //Pause
        Time.timeScale = 0;
        //15 second delay
        yield return new WaitForSecondsRealtime(15);
        //Resume
        Time.timeScale = 1;
        //Restarts
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}