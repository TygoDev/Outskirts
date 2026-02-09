using UnityEngine;
using UnityEngine.UI;

public class EquipmentButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null in EquipmentButton");
            return;
        }

        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;

        // Initialize correctly
        HandleGameStateChanged(
            GameManager.Instance.CurrentState,
            GameManager.Instance.CurrentState
        );
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    private void HandleGameStateChanged(GameState previous, GameState current)
    {
        button.interactable = current == GameState.Idle || current == GameState.Equiping;

        if(previous == GameState.Equiping)
        {
            GetComponent<ToggleMenu>().StatelessClose();
        }
    }
}
