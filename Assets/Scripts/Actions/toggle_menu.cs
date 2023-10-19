using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class toggle_menu : MonoBehaviour
{
    // V1 Riley
    // V2 Mackenzie 10/16/2023

    [SerializeField] 
    private CanvasGroup menu;

    GameObject blocker;
    Component[] buttons;

    private void Awake()
    {
        blocker = GameObject.Find("Raycast Blocker");
        buttons = menu.GetComponentsInChildren<UnityEngine.UI.Button>(true);
    }

    private void Start()
    {
        blocker.SetActive(false);

        foreach (UnityEngine.UI.Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void open()
    {
        blocker.SetActive(true);
        menu.alpha = 1;

        foreach (UnityEngine.UI.Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void close()
    {
        blocker.SetActive(false);
        menu.alpha = 0;

        foreach (UnityEngine.UI.Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == this.GetComponent<BoxCollider2D>() && !blocker.activeSelf)
            {
                open();
            }
        }
    }
}
