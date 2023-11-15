using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit_game : MonoBehaviour
{
    // Start is called before the first frame update


    public void exitGame()
    {
        Debug.Log("Test");
        Application.Quit();
        Debug.Log("Test2");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
