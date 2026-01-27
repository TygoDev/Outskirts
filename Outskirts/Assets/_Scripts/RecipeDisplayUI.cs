using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplayUI : MonoBehaviour
{
    public List<Image> itemsNeeded = new List<Image>();
    public Image resultItem;
    private Recipe selectedRecipe;

    public void SetSelectedRecipe(Recipe recipe)
    {   
        foreach(Image item in itemsNeeded)
        {
            item.gameObject.SetActive(false);
        }

        selectedRecipe = recipe;
        resultItem.sprite = recipe.resultItem.icon;

        for (int i = 0; i < itemsNeeded.Count; i++)
        {
            if (i < recipe.itemsNeeded.Count)
            {
                itemsNeeded[i].sprite = recipe.itemsNeeded[i].item.icon;
                itemsNeeded[i].GetComponentInChildren<TMPro.TMP_Text>().text = recipe.itemsNeeded[i].amount.ToString();
                itemsNeeded[i].gameObject.SetActive(true);
            }
            else
            {
                itemsNeeded[i].gameObject.SetActive(false);
            }
        }
    }

    public void Craft()
    {
        if (selectedRecipe != null)
        {
            Crafting crafting = (Crafting)GameManager.Instance.shippedComponent;
            crafting.Craft(selectedRecipe);
        }
    }
}
