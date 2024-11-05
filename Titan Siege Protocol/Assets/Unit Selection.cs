using UnityEngine;
using System.Collections.Generic;

public class UnitSelection : MonoBehaviour
{
    public GameObject unitSelectedPrefab;
    private GameObject selectedUnit;
    private GameObject status;
    //DONT FORGET: Layermask made clicking on units WAY EASIER
    public LayerMask unitLayerMask;

    public List<UnitMovement> selectedUnits = new List<UnitMovement>();

    //Checks every frame
    void Update() {
        //Left Click to select unit
        if(Input.GetMouseButtonDown(0)) {
            selectUnit();
        }
        //Right Click to move unit
        if(Input.GetMouseButtonDown(1) && selectedUnit != null) {
            moveUnit();
        }
    }

    void selectUnit() {
        //gives us where our mouse is currently at in game
        Vector2 cursorLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Checks if unit was clicked (unitLayer)
        RaycastHit2D hit = Physics2D.Raycast(cursorLocation, Vector2.zero, Mathf.Infinity, unitLayerMask);

        //Checks if whatever was hit had tag of "Unit"
        if(hit.collider != null && hit.collider.CompareTag("Unit")) {
            if(status != null) {
                Destroy(status);
            }

            //Our selected unit will now have red circle around it when selected (unitSelectedPrefab)
            selectedUnit = hit.collider.gameObject;
            status = Instantiate(unitSelectedPrefab);
            status.transform.position = selectedUnit.transform.position;
            status.transform.parent = selectedUnit.transform;
        }
        else {
            if (status != null) {
                Destroy(status);
            }
            selectedUnit = null;
        }

    }

    void moveUnit() {
        //Gives us location of where we want to go in game
        Vector2 targetLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Holds instructions of selectedUnit
        UnitMovement unitMovement = selectedUnit.GetComponent<UnitMovement>();

        //Our unit was selected and will move to the target location
        if(unitMovement != null) {
            unitMovement.SetTargetPosition(targetLocation);
            unitMovement.unitSelect(true);
        }
    }

        
}