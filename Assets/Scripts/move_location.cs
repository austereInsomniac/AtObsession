using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Mackenzie

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
    private GameObject bathroom;
    private GameObject bathroomCanvas;
    private GameObject kitchen;
    private GameObject kitchenCanvas;
    private GameObject livingRoom;
    private GameObject livingRoomCanvas;
    private GameObject gameOver;
    private GameObject gameOverCanvas;
    private GameObject mainMenu;
    private GameObject mainMenuCanvas;

    private List<Button> buttons;
    private List<Button> buttons2;
    private List<BoxCollider2D> colliders;
    private List<BoxCollider2D> colliders2;

    private bool isBlocked = false;
    private float displayTimeStatic = 1.25f;
    private float displayStartTime;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<game_state>();
        bedroom = GameObject.Find("Bedroom");
        bedroomCanvas = GameObject.Find("Bedroom Canvas");
        bathroom = GameObject.Find("Bathroom");
        bathroomCanvas = GameObject.Find("Bathroom Canvas");
        kitchen = GameObject.Find("Kitchen");
        kitchenCanvas = GameObject.Find("Kitchen canvas");
        livingRoom = GameObject.Find("Living Room");
        livingRoomCanvas = GameObject.Find("Living Room Canvas");
        gameOver = GameObject.Find("Game Over");
        gameOverCanvas = GameObject.Find("Game Over Canvas");
        mainMenu = GameObject.Find("Main Menu");
        mainMenuCanvas = GameObject.Find("Main Menu Canvas");

        buttons = new List<Button>();
        buttons2 = new List<Button>();
        Button[] buttons0 = GameObject.FindObjectsOfType<Button>();
        for (int i = 0; i < buttons0.Length; i++) {
            if (buttons0[i] != null)
            {
                buttons.Add(buttons0[i]);
            }
        }

        colliders = new List<BoxCollider2D>();
        colliders2 = new List<BoxCollider2D>();
        BoxCollider2D[] colliders0 = GameObject.FindObjectsOfType<BoxCollider2D>();
        for (int i = 0; i < colliders0.Length; i++)
        {
            if (colliders0[i] != null)
            {
                colliders.Add(colliders0[i]);
            }
        }

    }

    public GameObject getBedroom() {  return bedroom; }
    public GameObject getBedroomCanvas() {  return bedroomCanvas; }
    public GameObject getBathroom() { return bathroom; }
    public GameObject getBathroomCanvas() { return bathroomCanvas; }
    public GameObject getKitchen() { return kitchen; }
    public GameObject getKitchenCanvas() {  return kitchenCanvas; }
    public GameObject getLivingRoom() {  return livingRoom; }
    public GameObject getLivingRoomCanvas() {  return livingRoomCanvas; }
    public GameObject getGameOver() { return gameOver; }
    public GameObject getGameOverCanvas() {  return gameOverCanvas; }
    public GameObject getMainMenu() {  return mainMenu; }
    public GameObject getMainMenuCanvas() {  return mainMenuCanvas; }
    public GameObject getOther() { return other; }
    public GameObject getOtherCanvas() {  return otherCanvas; }

    public GameObject getThisO() {  return thisO; }
    public GameObject getThisCanvas() { return thisCanvas; }

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

    public void goToBathroom()
    {
        moveLocation(bathroom, bathroomCanvas, player.getLocation(), player.getLocationCanvas());
    }

    public void goToKitchen()
    {
        moveLocation(kitchen,kitchenCanvas, player.getLocation(), player.getLocationCanvas());
    }
    public void goToLivingRoom()
    {
        moveLocation(livingRoom, livingRoomCanvas, player.getLocation(), player.getLocationCanvas());
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
                displayStartTime = Time.timeSinceLevelLoad;
                StartCoroutine(OnButtonClicked());
            }
        } 
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad >= displayTimeStatic + displayStartTime && isBlocked == true)
        {
            foreach (Button button in buttons2)
            {
                button.interactable = true;
            }
            buttons2.Clear();

            /*foreach (BoxCollider2D coll in colliders2)
            {
                coll.enabled = true;
            }
            buttons2.Clear();*/

            isBlocked = false;
        }
    }

    IEnumerator OnButtonClicked()
    {
        foreach (Button button in buttons)
        {
            if(button.interactable == true)
            { 
                buttons2.Add(button);
                button.interactable = false;
            }
            
        }

        isBlocked = true;
        yield return new WaitForSeconds(.00001f);
    }
}