using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mackenzie 

public class swap_assets : MonoBehaviour
{
    SpriteRenderer other;
    SpriteRenderer thisObject;
    asset_highlighter highlightManager;

    private AudioSource audioSource; // Reference to the AudioSource component on this object
    public bool isSink; // Set this to true in the Inspector for the sink object


    public void Start()
    {
        // the alternate object must be the second child behind the highlight
        other = GetComponentsInChildren<SpriteRenderer>()[2];
        thisObject = GetComponent<SpriteRenderer>();
        highlightManager = GetComponent<asset_highlighter>();

        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();

        // Check if this object is the sink and has an AudioSource
        if (isSink && audioSource != null)
        {
            // Set the audio clip to loop for the sink
            audioSource.loop = true;
        }
    }

    // swaps the visibility of this objectc and the cooresponding object
    // each object will come in a pair where one is alwazys visible and one is always invisible
    public void swapObjects()
    {
        other.enabled = !other.enabled;
        thisObject.enabled = !thisObject.enabled;
        highlightManager.swapHighlight();

        // Check if the AudioSource component is attached and has an AudioClip
        if (audioSource != null && audioSource.clip != null)
        {
            // If it's the sink, start or stop the looping based on visibility
            if (isSink)
            {
                if (other.enabled)
                {
                    audioSource.Play(); // Start looping when the sink is visible
                }
                else
                {
                    audioSource.Stop(); // Stop looping when the sink is invisible
                }
            }
            else
            {
                // Play the sound once for non-sink objects
                audioSource.PlayOneShot(audioSource.clip);
            }
        }
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
                swapObjects();
            }
        }
    }
}
