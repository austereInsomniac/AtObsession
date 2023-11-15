using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class highlight_on_hover : MonoBehaviour
{
    //[SerializeField]
    //private GameObject highlighted;
    bool running = true;
    SpriteRenderer highlight;

    private void OnMouseOver()
    {
        

        if (running)
        {
            this.GetComponentInChildren<SpriteRenderer>().enabled = true;
            //highlight.enabled = true;
            running = false;
        }
    }

    private void OnMouseExit()
    {
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        //highlighted_sprite.enabled = false ;
        //highlight.enabled=false;
        running = true;

    }

    private void Awake()
    {
        //highlight = highlighted.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
}
