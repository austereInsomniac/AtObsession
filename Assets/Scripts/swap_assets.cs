using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mackenzie 

public class swap_assets : MonoBehaviour
{
    SpriteRenderer other;
    SpriteRenderer thisObject;
    asset_highlighter highlightManager;

    public void Start()
    {
        // the alternate object must be the second child behind the highlight
        other = GetComponentsInChildren<SpriteRenderer>()[2];
        thisObject = GetComponent<SpriteRenderer>();
        highlightManager = GetComponent<asset_highlighter>();
    }

    // swaps the visibility of this objectc and the cooresponding object
    // each object will come in a pair where one is alwazys visible and one is always invisible
    public void swapObjects()
    {
        other.enabled = !other.enabled;
        thisObject.enabled = !thisObject.enabled;   
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == this.GetComponent<BoxCollider2D>())
            {
                swapObjects();
                highlightManager.swapHighlight();
            }
        }
    }
}
