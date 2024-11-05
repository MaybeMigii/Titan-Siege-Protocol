
using UnityEngine;
//This isn't really used, was just for testing purposes until swapped it out
public class Unit : MonoBehaviour {
    private SpriteRenderer spriteRenderer; // Reference to the unit's SpriteRenderer component
    private Color originalColor;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Store the original color of the unit
    }

    public void ToggleSelection(bool picked) {
        if (picked) {
            spriteRenderer.color = Color.green; // Change to green when selected
        }
        else {
            spriteRenderer.color = originalColor; // Revert to original color when deselected
        }
    }
}