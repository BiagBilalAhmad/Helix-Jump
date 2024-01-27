using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItem : MonoBehaviour
{
    [SerializeField] ShopItemType shopItemType;
    [SerializeField] int id;
    [SerializeField] int cost;
    [SerializeField] GameObject costObject;
    [SerializeField] GameObject selectedImage;
    [SerializeField] GameObject BGForTheme;
    [SerializeField] TMP_Text costText;
    public GameObject Panel;
    private bool isPanelActive=true;
    private void OnEnable()
    {
        if (shopItemType == ShopItemType.Car)
        {
            cost = menuManager.carCost[id];
        }
        else
        {
            //cost = GameManager.themeCost[id];
        }
        costText.text = cost.ToString();

        UpdateState();
    }
    public void ItemClick()
    {
     //   AudioManager.Instance.PlaySFX(SFXType.button);

        if (shopItemType == ShopItemType.Car)
        {
         //   Panel.SetActive(true);
            if (PlayerPrefs.GetInt("Car" + id, 0) == 1)
            {
                PlayerPrefs.SetInt("SelectCar", id);
            }
            else
            {
                if (PlayerPrefs.GetInt("TC", 0) >= cost)
                {
                    ;//   AudioManager.Instance.PlaySFX(SFXType.unlockCar);
                    PlayerPrefs.SetInt("Car" + id, 1);
                    PlayerPrefs.SetInt("SelectCar", id);
                    PlayerPrefs.SetInt("TC", PlayerPrefs.GetInt("TC", 0) - cost);
                }
            }

        }
        else
        {
            if (PlayerPrefs.GetInt("Theme" + id, 0) == 1)
            {
                PlayerPrefs.SetInt("SelectTheme", id);
            }
            else
            {
                if (PlayerPrefs.GetInt("TC", 0) >= cost)
                {
                   // AudioManager.Instance.PlaySFX(SFXType.unlockThemes);

                    PlayerPrefs.SetInt("Theme" + id, 1);
                    PlayerPrefs.SetInt("SelectTheme", id);
                    PlayerPrefs.SetInt("TC", PlayerPrefs.GetInt("TC", 0) - cost);
                }
            }
        }
        menuManager.Instance.UpdateTextScore();
        //UpdateState();
        UpdateAllItem();
    }
    public void UpdateState()
    {
        if (shopItemType == ShopItemType.Car)
        {
            if (PlayerPrefs.GetInt("Car" + id, 0) == 1)
            {
                costObject.gameObject.SetActive(false);
            }
            else
            {
                costObject.gameObject.SetActive(true);
            }
            selectedImage.SetActive(PlayerPrefs.GetInt("SelectCar", 0) == id);

        }
        else
        {
            if (PlayerPrefs.GetInt("Theme" + id, 0) == 1)
            {
                costObject.gameObject.SetActive(false);
            }
            else
            {
                costObject.gameObject.SetActive(true);
            }
            selectedImage.SetActive(PlayerPrefs.GetInt("SelectTheme", 0) == id);
            BGForTheme.SetActive(false);
            BGForTheme.SetActive(true);
        }
    }
    void UpdateAllItem()
    {
        ShopItem[] shopItems = FindObjectsOfType<ShopItem>();
        foreach (ShopItem shopItem in shopItems)
            shopItem.UpdateState();
    }
}
enum ShopItemType
{
    Car,
    Theme
}
