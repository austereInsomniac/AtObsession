using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mackenzie 

public class swap_background_assets : MonoBehaviour
{
    [SerializeField]
    GameObject other;

    // swaps the visibility of this objectc and the cooresponding object
    // each object will come in a pair where one is alwazys visible and one is always invisible
    public void swapObjects()
    {
        other.GetComponent<SpriteRenderer>().enabled = !other.GetComponent<SpriteRenderer>().enabled;
        this.GetComponent<SpriteRenderer>().enabled = !this.GetComponent<SpriteRenderer>().enabled;   
    }
}
