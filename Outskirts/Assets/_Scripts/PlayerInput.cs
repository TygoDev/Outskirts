using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Canvas interactCanvas = null;
    private bool isOverlapping = false;
    private GameObject overlappingObject = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractableObject"))
        {
            interactCanvas.gameObject.SetActive(true);
            isOverlapping = true;
            overlappingObject = collision.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractableObject"))
        {
            interactCanvas.gameObject.SetActive(false);
            isOverlapping = false;
            if(overlappingObject.GetComponent<ToggleMenu>() != null)
            overlappingObject.GetComponent<ToggleMenu>().Close();
            overlappingObject = null;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {

        if (context.canceled)
        {
            if (isOverlapping && overlappingObject != null)
            {
                overlappingObject.GetComponent<ToggleMenu>().Toggle();
            }
        }
    }
}
