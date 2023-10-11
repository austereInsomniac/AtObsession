using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_through_door : MonoBehaviour
{

    public Vector2 bedroom = new Vector2(33, 0);
    public Vector2 bathroom = new Vector2(4116, 0);
    public Vector2 kitchen = new Vector2(1039, 0);
    public Vector2 livingRoom = new Vector2(2066, 0);

    public GameObject player;
    public GameObject bathroomDoorEnter;
    public GameObject bedroomDoorEnter;
    public GameObject kitchenEnter;
    public GameObject bedroomLeave;
    public GameObject kitchenLeave; 
    public GameObject leaveHouse;
    public GameObject leaveDoor;

    private void Start()
    {

    }


    public void OnMouseDown()
    {
        Debug.Log("Click");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider == bathroomDoorEnter.GetComponent<BoxCollider2D>())
            {
                Debug.Log("Something was clicked");
                player.transform.position = bathroom;
            }

            if(hit.collider == bedroomDoorEnter.GetComponent<BoxCollider2D>())
            {
                player.transform.position = bedroom;
            }

            if (hit.collider == bedroomLeave.GetComponent<BoxCollider2D>())
            {
                player.transform.position = livingRoom;

            }

            if (hit.collider == kitchenEnter.GetComponent<BoxCollider2D>())
            {
                player.transform.position = kitchen;
            }

            if (hit.collider == kitchenLeave.GetComponent<BoxCollider2D>())
            {
                player.transform.position = livingRoom;
            }
        } 
    }
}