using System.Collections;
using System.Collections.Generic;
<<<<<<< Updated upstream
=======
using System.Runtime.CompilerServices;
using UnityEditor.SceneTemplate;
>>>>>>> Stashed changes
using UnityEngine;

public class move_through_door : MonoBehaviour
{
    private Vector3 storedLocation = new Vector3(2000, 0, 0);
    private Vector3 cameraLocation = new Vector3(0, 0, 0);

<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnMouseDown()
=======
    [SerializeField]
    private GameObject other; 

    private void moveLocation(int moveFrom, int moveTo)
    {
       Vector3 locationFrom = new Vector3(moveFrom, 0, 0);
       Vector3 locationTo = new Vector3(moveTo, 0, 0);
        // move this away
        this.transform.position = storedLocation;
        // move away olf from camera
        other.transform.position = cameraLocation;
    }

   /* public void OnMouseDown()
>>>>>>> Stashed changes
    {
        Vector3 pos = new Vector3(0, 0, 5);
        Debug.Log("Click");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null)
            {
                Debug.Log("Something was clicked");
                Camera.main.transform.position = pos;
            }


        } 
    }*/
}