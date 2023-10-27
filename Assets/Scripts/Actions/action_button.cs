using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action_button : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public void OnGUI()
    {
        // this must take priority over shower
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == this.GetComponent<BoxCollider2D>())
            {
                if (!this.name.Equals("Exit"))
                {
                    this.GetComponent<object_for_all_variables>().doAction();
                }
                menu.GetComponent<toggle_menu>().close();
                Event.current.Use();
            }
        }
    }
}
