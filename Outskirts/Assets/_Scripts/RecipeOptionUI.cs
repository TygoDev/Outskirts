using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeOptionUI : MonoBehaviour
{
    private RecipeDisplayUI recipeDisplayUI;

    [SerializeField] private Recipe recipe;

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    public void SetRecipe(RecipeDisplayUI recipeDisplayUI, Recipe recipe)
    {
        this.recipeDisplayUI = recipeDisplayUI;
        this.recipe = recipe;
        icon.sprite = recipe.resultItem.icon;
        itemName.text = recipe.resultItem.itemName;
        itemDescription.text = recipe.resultItem.description;
    }

    public void OnClick_RecipeButton()
    {
        recipeDisplayUI.SetSelectedRecipe(recipe);
    }
}
