using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnClick : MonoBehaviour
{
    // V1 Riley
    // V2 Mackenzie 10/16/2023

    [SerializeField] private CanvasGroup Menu;

    public void open()
    {
        Menu.alpha = 1;
        Menu.blocksRaycasts = true;
    }

    public void close()
    {
        Menu.alpha = 0;
        Menu.blocksRaycasts = false;
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
