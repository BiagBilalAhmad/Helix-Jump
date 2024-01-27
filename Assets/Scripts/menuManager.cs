using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.SceneManagement;

using DG.Tweening;

public class menuManager : MonoBehaviour
{
    public static int[] carCost = { 0, 1500, 4000, 5000, 8000, 10000, 13500, 1500, 17000, 20000 };
    public static menuManager Instance;
    public TMP_Text TotalCoins_txt;



    public RectTransform ShopBtn, EndlessBtn, LevelBtn;
    public GameObject Shop;
    private void Awake()
    {
        Time.timeScale = 1f;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        UpdateTextScore();
    }
    private void Start()
    {
        ANimateButtons();
    }

    public void ANimateButtons()
    {
        ShopBtn.DOAnchorPosY(-248f, .5f).SetEase(Ease.Flash).SetUpdate(true);
        EndlessBtn.DOAnchorPosY(268f, .5f).SetEase(Ease.Flash).SetUpdate(true);
        LevelBtn.DOAnchorPosY(268f, .5f).SetEase(Ease.Flash).SetUpdate(true);
    }


    public void ShopButon()
    {
        ShopBtn.DOAnchorPosY(728f, .5f).SetEase(Ease.Flash).SetUpdate(true);
        EndlessBtn.DOAnchorPosY(-827f, .5f).SetEase(Ease.Flash).SetUpdate(true);
        LevelBtn.DOAnchorPosY(-827f, .5f).SetEase(Ease.Flash).SetUpdate(true).OnComplete(
            () =>  Shop.SetActive(true)

            ); ;
    }
    public void UpdateTextScore()
    {
        TotalCoins_txt.text ="Total Coins : "+ PlayerPrefs.GetInt("TC", 0).ToString();
    }
    public void LevelBase()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("IsLevel", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Endless()
    {
        Time.timeScale = 1f;

        PlayerPrefs.SetInt("IsLevel", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
}
