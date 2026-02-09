using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameState newState;
    [SerializeField] private Component componentToShip;
    [SerializeField] private bool changeState = false;

    private void Start()
    {
        Close();
    }

    public void Toggle()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(!menuUI.activeSelf);

            if (!changeState)
                return;

            if (menuUI.activeSelf)
            {
                GameManager.Instance.SetState(newState, componentToShip);
            }
            else
            {
                GameManager.Instance.SetState(GameState.Idle);
            }
        }
    }

    public void Close()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(false);
            GameManager.Instance.SetState(GameState.Idle);
        }
    }

    public void StatelessClose()
    {
        menuUI.SetActive(false);
    }

    public bool isOpen()
    {
        return menuUI.activeSelf;
    }
}
