using System;
using UnityEngine;

public class SetRecipeOptionsUI : MonoBehaviour
{
    [SerializeField] private GameObject recipeSlotPrefab = null;
    [SerializeField] private RecipeDisplayUI recipeDisplayUI = null;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged += Populate;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= Populate;
    }

    private void Populate(GameState state1, GameState state2)
    {
        if (state2 != GameState.Crafting)
            return;

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        Crafting crafting = (Crafting)GameManager.Instance.shippedComponent;
        foreach (Recipe recipe in crafting.recipes)
            {
                GameObject clone = Instantiate(recipeSlotPrefab, gameObject.transform);
                clone.GetComponent<RecipeOptionUI>().SetRecipe(recipeDisplayUI, recipe);
            }
    }
}
