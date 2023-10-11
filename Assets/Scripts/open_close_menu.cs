using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnClick : MonoBehaviour
{

    [SerializeField] private CanvasGroup Menu;

    // Start is called before the first frame update
    public void OpenClose()
    {
        Menu.alpha = Menu.alpha > 0 ? 0 : 1;
        Menu.blocksRaycasts = Menu.blocksRaycasts == false ? true : false;
    }
}
