using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class toggle_menu : MonoBehaviour
{
    // Mackenzie 

    [SerializeField] 
    private CanvasGroup menu;

    private UnityEngine.UI.Image menuBlocker;
    private BoxCollider2D menuCollider;
    private Component[] buttons;

    private void Awake()
    {
        menuBlocker = GameObject.Find("Menu Click Blocker").GetComponent<UnityEngine.UI.Image>();
        menuCollider = GameObject.Find("Menu Click Blocker").GetComponent<BoxCollider2D>();
        buttons = menu.GetComponentsInChildren<UnityEngine.UI.Button>(true);
    }

    public void open()
    {
        menu.alpha = 1;
        menu.blocksRaycasts = true;
        menuBlocker.enabled = true;
        menuCollider.enabled = true;
    }

    public void close()
    {
        menu.alpha = 0;
        menu.blocksRaycasts = false;
        menuBlocker.enabled = false;
        menuCollider.enabled = false;
    }

    public void closeWithBlocker()
    {
        menu.alpha = 0;
        //menu.blocksRaycasts = false;
        //menuBlocker.enabled = false;
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
                open();
            }
        }
    }
}
