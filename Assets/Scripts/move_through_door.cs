using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class move_through_door : MonoBehaviour
{

    public Vector3 bedroomLocation = new Vector3(0, 0, 0);
    public Vector3 bathroomLocation = new Vector3(0, 0, 5);
    public Vector3 kitchenLocation = new Vector3(0, 0, 10);
    public Vector3 livingRoomLocation = new Vector3(0, 0, 20);
    public Vector3 computerLocation = new Vector3(0, 0, 15);

    public GameObject livingRoom;
    public GameObject bedroom;
    public GameObject bathroom;
    public GameObject kitchen;
    
    
    public GameObject player;
    public GameObject bathroomDoorEnter;
    public GameObject bedroomDoorEnter;
    public GameObject kitchenEnter;
    public GameObject bedroomLeave;
    public GameObject kitchenLeave; 
    public GameObject leaveHouse;
    

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
                Vector3 temp = bathroom.transform.position;
                bathroom.transform.position = livingRoom.transform.position;
                livingRoom.transform.position = temp;
            }

            if(hit.collider == bedroomDoorEnter.GetComponent<BoxCollider2D>())
            {
                Debug.Log("Something was clicked");
                Vector3 temp = bedroom.transform.position;
                bedroom.transform.position = livingRoom.transform.position;
                livingRoom.transform.position = temp;
            }

            if (hit.collider == bedroomLeave.GetComponent<BoxCollider2D>())
            {
                Debug.Log("Something was clicked");
                Vector3 temp = bedroom.transform.position;
                bedroom.transform.position = livingRoom.transform.position;
                livingRoom.transform.position = temp;

            }

            if (hit.collider == kitchenEnter.GetComponent<BoxCollider2D>())
            {
                Debug.Log("Something was clicked");
                Vector3 temp = kitchen.transform.position;
                kitchen.transform.position = livingRoom.transform.position;
                livingRoom.transform.position = temp;
            }

            if (hit.collider == kitchenLeave.GetComponent<BoxCollider2D>())
            {
                Debug.Log("Something was clicked");
                Vector3 temp = kitchen.transform.position;
                kitchen.transform.position = livingRoom.transform.position;
                livingRoom.transform.position = temp;
            }
        } 
    }
}