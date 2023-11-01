using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class highlight_on_hover : MonoBehaviour
{
    [SerializeField]
    private swap_background_assets s;
    bool running = true;

    private void OnMouseOver()
    {

        if (running)
        {
            s.swapObjects();
            running = false;
        }
    }

    private void OnMouseExit()
    {
        //highlighted_sprite.enabled = false ;
        s.swapObjects();
        running = true;

    }

    private void Awake()
    {

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
