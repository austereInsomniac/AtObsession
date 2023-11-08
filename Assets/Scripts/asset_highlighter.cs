using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class asset_highlighter : MonoBehaviour
{
    private SpriteRenderer highlight;

    // Start is called before the first frame update
    void Start()
    {
        highlight = GetComponentsInChildren<SpriteRenderer>()[1];
    }

    private void OnMouseEnter()
    {
        highlight.enabled = true;
    }

    private void OnMouseExit()
    {
        highlight.enabled = false;
    }
}
