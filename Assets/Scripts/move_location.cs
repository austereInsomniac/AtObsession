using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private game_state player;
    private GameObject bedroom;
    private GameObject bedroomCanvas;
    private GameObject gameOver;
    private GameObject gameOverCanvas;
    private GameObject mainMenu;
    private GameObject mainMenuCanvas;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<game_state>();
        bedroom = GameObject.Find("Bedroom");
        bedroomCanvas = GameObject.Find("Bedroom Canvas");
        gameOver = GameObject.Find("Game Over");
        gameOverCanvas = GameObject.Find("Game Over Canvas");
        mainMenu = GameObject.Find("Main Menu");
        mainMenuCanvas = GameObject.Find("Main Menu Canvas");
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
        player.moveLocation(other_, otherCanvas_);
    }

    public void goToBedroom(){
        moveLocation(bedroom, bedroomCanvas, player.getLocation(), player.getLocationCanvas());
    }

    public void goToGameOver()
    {
        moveLocation(gameOver, gameOverCanvas, player.getLocation(), player.getLocationCanvas());
    }

    public void goToMainMenu()
    {
        moveLocation(mainMenu,mainMenuCanvas, player.getLocation(), player.getLocationCanvas());
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