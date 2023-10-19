using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class move_location : MonoBehaviour
{
    private Vector3 storedLocation = new Vector3(2000, 0, 0);
    private Vector3 cameraLocation = new Vector3(0, 0, 0);

    [SerializeField]
    private GameObject other;
    [SerializeField]
    private GameObject otherCanvas;
    [SerializeField]
    private GameObject thisO;
    [SerializeField]
    private GameObject thisCanvas;

    private GameObject player;
    private GameObject bedroom;
    private GameObject bedroomCanvas;
    //private GameObject blocker;

    private void Awake()
    {
        player = GameObject.Find("Player");
        bedroom = GameObject.Find("Bedroom");
        bedroomCanvas = GameObject.Find("Bedroom Canvas");
        //blocker = GameObject.Find("Raycast Blocker");
    }
    public void moveLocation(GameObject other_, GameObject otherCanvas_, GameObject this_, GameObject thisCanvas_)
    {
        // move this room away
        this_.transform.position = storedLocation;
        thisCanvas_.transform.position = storedLocation;

        // move new room in 
        other_.transform.position = cameraLocation;
        otherCanvas_.transform.position = cameraLocation;

        // update location in game state
        player.GetComponent<game_state>().moveLocation(other, otherCanvas);
    }

    public void goToBedroom(){
        moveLocation(bedroom, bedroomCanvas, player.GetComponent<game_state>().getLocation(), 
            player.GetComponent<game_state>().getLocationCanvas());
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider == this.GetComponent<BoxCollider2D>())
            {
                moveLocation(other, otherCanvas, thisO, thisCanvas);
            }
        } 
    }

}