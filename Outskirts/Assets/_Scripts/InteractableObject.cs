using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject menuToTrigger = null;

    public void TriggerInteraction()
    {
        if (menuToTrigger != null)
        {
            menuToTrigger.SetActive(true);
        }
    }
}
