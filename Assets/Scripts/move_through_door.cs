using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_through_door : MonoBehaviour
{

    public Vector3 bedroom = new Vector3(33, 0, -10);
    public Vector3 bathroom = new Vector3(4116, 0, -10);
    public Vector3 kitchen = new Vector3(1039, 0, -10);
    public Vector3 livingRoom = new Vector3(2066, 0, -10);

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
                Debug.Log("Something was clicked");
                player.transform.position = bedroom;
            }

            if (hit.collider == bedroomLeave.GetComponent<BoxCollider2D>())
            {
                Debug.Log("Something was clicked");
                player.transform.position = livingRoom;

            }

            if (hit.collider == kitchenEnter.GetComponent<BoxCollider2D>())
            {
                Debug.Log("Something was clicked");
                player.transform.position = kitchen;
            }

            if (hit.collider == kitchenLeave.GetComponent<BoxCollider2D>())
            {
                Debug.Log("Something was clicked");
                player.transform.position = livingRoom;
            }
        } 
    }
}