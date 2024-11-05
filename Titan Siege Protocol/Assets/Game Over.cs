using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//WIP 11/4
//As of right now there is no game over screen
//After 5 titans enter the gate, the game just restarts
public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TextMeshProUGUI gameOverText;
    //Need to be sure that our gameOverScreen doesn't show up at the beginning
    //But after we've lost so it's inactive right now
    void Start() {
        gameOverScreen.SetActive(false);
    }

    public void showGameOver() {
        gameOverScreen.SetActive(true);
        gameOverText.text = "Game Over! Humanity Was Eaten.";
    }


}
