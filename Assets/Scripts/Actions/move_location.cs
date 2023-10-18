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
    private GameObject thisCanvas;

    private GameObject player;
    private GameObject bedroomCanvas;

    private void Start()
    {
        player = GameObject.Find("Player");
        bedroomCanvas = GameObject.Find("Bedroom Canvas");
    }
    public void moveLocation(GameObject other, GameObject otherCanvas, GameObject thisCanvas)
    {
        // move this room away
        this.transform.parent.parent.position = storedLocation;
        thisCanvas.transform.position = storedLocation;

        // move new room in 
        other.transform.position = cameraLocation;
        otherCanvas.transform.position = cameraLocation;

        // update location in game state
        player.GetComponent<game_state>().moveLocation(other, otherCanvas);
    }

    public void goToBedroom(){
        moveLocation(player.GetComponent<game_state>().getLocation(), 
            player.GetComponent<game_state>().getLocationCanvas(), bedroomCanvas);
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
                moveLocation(other, otherCanvas, thisCanvas);
            }
        } 
    }

}