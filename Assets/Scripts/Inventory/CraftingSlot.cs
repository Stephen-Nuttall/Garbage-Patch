using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    [Header("Crafting Information")]
    [SerializeField] Item[] ingredients;
    [SerializeField] int[] ingredientQuantities;
    [SerializeField] Item resultItem;
    [SerializeField] int resultQuantity = 1;

    [Header("UI Information")]
    [SerializeField] Image[] ingredientIcons;
    [SerializeField] TMP_Text[] ingredientTexts;
    [SerializeField] Image resultIcon;
    [SerializeField] TMP_Text resultText;

    void Awake()
    {
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        if (resultItem == null)
        {
            // Debug.Log("No result item set for this crafting slot. Disabling");
            gameObject.SetActive(false);
        }
        else
        {
            resultIcon.sprite = resultItem.GetImage();
            resultText.text = resultQuantity + " " + resultItem.name;

            for (int i = 0; i < ingredientIcons.Length; i++)
            {
                if (ingredients[i] != null)
                {
                    ingredientIcons[i].enabled = true;
                    ingredientTexts[i].enabled = true;

                    ingredientIcons[i].sprite = ingredients[i].GetImage();
                    ingredientTexts[i].text = ingredientQuantities[i] + " " + ingredients[i].name;
                }
                else
                {
                    ingredientIcons[i].enabled = false;
                    ingredientTexts[i].enabled = false;
                }
            }
        }
    }

    public void Craft()
    {
        InventoryManager inventoryManager = FindAnyObjectByType<InventoryManager>();

        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null)
            {
                if (!inventoryManager.ConsumeItem(ingredients[i], ingredientQuantities[i]))
                {
                    return;
                }
            }
        }

        inventoryManager.AddItem(resultItem, resultQuantity);
        RefreshDisplay();
    }
}
