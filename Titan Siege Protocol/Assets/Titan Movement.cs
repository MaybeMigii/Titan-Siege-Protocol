using UnityEngine;

public class TitanMovement : MonoBehaviour
{
    public Vector2 targetPosition;
    public float movementSpeed = 5f;
    //distance from where our titan will be destroyed when approaching
    //target area (open gate)
    public float destroyDistance = 0.5f;
    public float unitDetectionDistance = 5f;
    public LayerMask unitLayerMask;
    private Transform nearestUnit;
    public float stopDistanceFromUnit = 1f;
    public Health healthTracker;
    //Need to hold on to health component for laters
    void Start() {
    healthTracker = FindObjectOfType<Health>();
    }
    void Update() {
        huntForHuman();

        Vector2 currentTarget;
        //Will move towards the nearest unit that is found
        if(nearestUnit != null) {
            float distanceFromUnit = Vector2.Distance(transform.position, nearestUnit.position);
            //This is how close our titan can get to unit
            if(distanceFromUnit > stopDistanceFromUnit) {
                currentTarget = nearestUnit.position;
            }
            else {
                //titan wont move any closer
                return;
            }
        } 
        else {
            //Continues going towards the broken gate
            currentTarget = targetPosition;
        }

        float velocity = movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, velocity);
        
        //Once titan is close enough to the broken gate it will
        //bring down our health by 1 and titan will be destroyed(looks as if enters gate)
        if(Vector2.Distance(transform.position, targetPosition) <= destroyDistance) {
            healthTracker.titanEnters();
            Destroy(gameObject);
        }
    }

    void huntForHuman() {
        //Tracking for units within a certain radius
        Collider2D humanNearBy = Physics2D.OverlapCircle(transform.position, unitDetectionDistance, unitLayerMask);
        if(humanNearBy != null) {
            nearestUnit = humanNearBy.transform;
        }
        else {
            nearestUnit = null;
        }
    }
}