using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour {
    public int totalHealth = 5;
    private int currentHealth;
    public TextMeshProUGUI healthText;

    public void Start() {
        currentHealth = totalHealth;
        refreshHealth();
    }

    //Don't need for now, issue with gate health displaying fixed
    // public void Update() {
    //     refreshHealth();
    // }

    //Whenever a titan enters we want the health (Bottom left corner of game)
    //To decrement by a value of 1
    //When health reaches 0, game forcibly restarts
    public void titanEnters() {
        if(currentHealth > 0) {
            currentHealth--;
            refreshHealth();
            if(currentHealth <= 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    //Updates our text on the UI
    private void refreshHealth() {
        healthText.text = currentHealth.ToString();
    }
}
