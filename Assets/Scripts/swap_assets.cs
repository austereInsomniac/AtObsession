using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mackenzie 

public class swap_assets : MonoBehaviour
{
    SpriteRenderer other;
    SpriteRenderer thisObject;

    public void Start()
    {
        other = GetComponentsInChildren<SpriteRenderer>()[0];
        thisObject = GetComponent<SpriteRenderer>();
    }

    // swaps the visibility of this objectc and the cooresponding object
    // each object will come in a pair where one is alwazys visible and one is always invisible
    public void swapObjects()
    {
        other.enabled = !other.enabled;
        this.enabled = !thisObject.enabled;   
    }
}
