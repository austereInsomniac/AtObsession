using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_through_door : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnMouseDown()
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
    }
}