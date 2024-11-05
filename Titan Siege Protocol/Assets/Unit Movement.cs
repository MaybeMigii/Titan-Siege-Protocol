using System.Collections;
using UnityEngine;

public class UnitMovement : MonoBehaviour {
    public float movementSpeed = 5f;
    //Spacing of units when I move them (so that they aren't standing directly on each other)
    public float spacing = 2f; 
    //Location where I want unit to move towards
    private Vector2 targetPosition;
    private bool unitPicked = false; 
    private Transform nearestTitan;

    //For when unit is circling Titan
    private float angle = 0f;
    public float orbitSpeed = 2f;
    public float orbitDistance = 1f;

    public LayerMask titanLayerMask;
    private bool fighting = false;
    //don't want fights to last too long
    public float combatTime = 5f;
    public GameObject humanBloodPrefab;
    public GameObject titanBloodPrefab;

    private AudioSource audioSource;
    //Audio for when unit killed
    public AudioClip screamBeforeDeath;
    //Audio for when titan killed
    public AudioClip giveYourHeart;
    void Start() {
        titanLayerMask = LayerMask.GetMask("Titan");
        audioSource = GetComponent<AudioSource>();
    }
    void Update() {
        //Checks if any titans are within a certain distance from unit
        titanNearby();

        //If titan is near, unit attacks
        if(nearestTitan != null) {
            if(!fighting) {
                StartCoroutine(combatDecider());
            }
            attackTitan();
        }
        else if (unitPicked == true) {
            // Only move if the unit is picked to be moved
            //NOTE: Vector2 MoveTowards(Vector2 current (Location), Vector2 target (Location), float maxDistanceDelta);
            float velocity = movementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocity);
        }
    }

    public void unitSelect(bool isSelected) {
        unitPicked = isSelected;
    }

    // Method to set a new target position
    public void SetTargetPosition(Vector2 goal) {
        //Don't want units standing on each other so there's an offset
        //To where they'll stand if I click on a specific position
        float offsetX = (Random.value * 2 - 1) * spacing;
        float offsetY = (Random.value * 2 - 1) * spacing;
        targetPosition = goal + new Vector2(offsetX, offsetY);
        
    }

    public void titanNearby() {
        Collider2D titanNearBy = Physics2D.OverlapCircle(transform.position, orbitDistance, titanLayerMask);
        //There is a titan nearby
        if(titanNearBy != null) {
            nearestTitan = titanNearBy.transform;
            //Plays fighting(spinning) audio when titan is nearby to
            //represent unit being in combat
            if(audioSource.isPlaying == false) {
                audioSource.Play();
            }
        }
        else {
            //once no titan nearby audio stops
            nearestTitan = null;
            audioSource.Stop();
        }
    }

    public void attackTitan() {
        //go around a specific point in smooth fashion
        angle += orbitSpeed * Time.deltaTime;

        //hate trig
        float offsetX = Mathf.Cos(angle) * orbitDistance;
        float offsetY = Mathf.Sin(angle) * orbitDistance;

        //Circling around titan (represents unit in combat)
        transform.position = new Vector2(
            nearestTitan.position.x + offsetX,
            nearestTitan.position.y + offsetY
        );


    }
    private IEnumerator combatDecider() {
        fighting = true;
        //Needed delay between when unit and titan fight and whoever dies
        yield return new WaitForSeconds(combatTime);

        //Each unit and titan generate a random number between 1 - 100
        int unitValue = Random.Range(1,101);
        int titanValue = Random.Range(1,101);

        //If unit's value is higher, titan dies and blood shows
        if(unitValue > titanValue) {
            audioSource.PlayOneShot(giveYourHeart);
            yield return new WaitForSeconds(giveYourHeart.length);
            Instantiate(titanBloodPrefab, nearestTitan.position, Quaternion.identity);
            Destroy(nearestTitan.gameObject);
        }
        else {
            //If titan's value is higher, human dies and blood shows
            audioSource.PlayOneShot(screamBeforeDeath);
            yield return new WaitForSeconds(screamBeforeDeath.length);
            Instantiate(humanBloodPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        fighting = false;
    }

}