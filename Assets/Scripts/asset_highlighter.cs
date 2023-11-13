using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Mackenzie

public class asset_highlighter : MonoBehaviour
{
    private SpriteRenderer highlight = null;
    private SpriteRenderer highlight2 = null;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentsInChildren<SpriteRenderer>().Length > 1)
        {
            highlight = GetComponentsInChildren<SpriteRenderer>()[1];
        }
        
        // this is for objects with two different sprites, they must be in spot 3
        if (GetComponentsInChildren<SpriteRenderer>().Length > 3)
        {
            highlight2 = GetComponentsInChildren<SpriteRenderer>()[3];
        }
    }

    private void OnMouseEnter()
    {
        highlight.enabled = true;
    }

    private void OnMouseExit()
    {
        highlight.enabled = false;
    }

    public void swapHighlight()
    {
        // this is for objects with two different sprites, they must be in spot 3
        if (highlight2 != null)
        {
            highlight.enabled = false;
            SpriteRenderer temp = highlight;
            highlight = highlight2;
            highlight2 = temp;
            highlight.enabled = true;
        }
    }
}
