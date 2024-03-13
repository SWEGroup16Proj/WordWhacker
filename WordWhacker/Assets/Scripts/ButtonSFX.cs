using UnityEngine;
using UnityEngine.EventSystems; // Required for Event Systems.

// This script plays sounds when the mouse hovers over or clicks a button.
public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource audioSource; // Assign in the inspector.
    public AudioClip hoverSound; // Assign in the inspector.
    public AudioClip clickSound; // Assign in the inspector.

    // Function to call when the mouse pointer enters the button area.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource != null && hoverSound != null)
        {
            audioSource.clip = hoverSound;
            audioSource.Stop(); // Ensure any currently playing sound is stopped.
            audioSource.Play(); // Play the hover sound.
        }
        else
        {
            Debug.LogWarning("AudioSource or hoverSound not assigned.");
        }
    }

    // Function to call when the button is clicked.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.clip = clickSound;
            audioSource.Stop();
            audioSource.Play(); // Play the click sound.
        }
        else
        {
            Debug.LogWarning("AudioSource or clickSound not assigned.");
        }
    }
}
