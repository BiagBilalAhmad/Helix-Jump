using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;
public class GameMnanger : MonoBehaviour
{

    public GameObject Cylinder;
    public GameObject EndPlatform;
    public int NumberOfRings=10;
    public float MaxDistance=5;
    [HideInInspector]
    public float yPosition;

    public static GameMnanger Instance;
    public GameObject[] Balls;

    public Slider LevelSlider;

    public GameObject GamerOver;
    public GameObject NextButton;
    public Image Emoji;
    public Sprite SadEmoji, HappyEmoji;


    public TMP_Text Number1Txt, Number2Txt;
    private void Awake()
    {

        Instantiate(Balls[PlayerPrefs.GetInt("SelectCar", 0)], 
            Balls[PlayerPrefs.GetInt("SelectCar", 0)].transform.position, 
            Balls[PlayerPrefs.GetInt("SelectCar", 0)].transform.rotation);


        Number1Txt.text = PlayerPrefs.GetInt("N1", 1).ToString();
        Number2Txt.text = PlayerPrefs.GetInt("N2", 2).ToString();
        NumberOfRings = PlayerPrefs.GetInt("NR", 10);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        SetSlider(NumberOfRings - 1);
    }



    public void GameOverScreen(bool ISwin)
    {
        StartCoroutine(StartGameOVer(ISwin));
    }


    public IEnumerator StartGameOVer(bool Val)
    {
        yield return new WaitForSecondsRealtime(.5f);
        if (Val)
        {
            GamerOver.SetActive(true);
            GamerOver.transform.DOScale(1f, .5f).SetEase(Ease.Flash).SetUpdate(true);
            NextButton.SetActive(true);
            Emoji.sprite = HappyEmoji;

        }
        else
        {
            GamerOver.SetActive(true);
            GamerOver.transform.DOScale(1f, .5f).SetEase(Ease.Flash).SetUpdate(true);
            NextButton.SetActive(false);
            Emoji.sprite = SadEmoji;
        }
    }
    public void SetSlider(int val)
    {
        LevelSlider.maxValue = val;
    }

    public void UpdateSlider()
    {
        LevelSlider.value += 1;
    }
    private void Start()
    {
        yPosition = -2;
        for(int i=0;i<NumberOfRings-1;i++)
        {
            SpawnRings(Cylinder);
        }

        if(!LevelManager.Instance.IsEndless)
        {
            SpawnRings(EndPlatform);
        }
       
    }

    public void SpawnRings(GameObject Ring)
    {
       GameObject Newring= Instantiate(Ring, new Vector3(transform.position.x, yPosition, transform.position.z), Quaternion.identity) as GameObject;
        yPosition -= MaxDistance;

        Newring.transform.parent = transform;
    }


    public void CallNextButton()
    {
        PlayerPrefs.SetInt("N1", PlayerPrefs.GetInt("N1", 1) + 1);
        PlayerPrefs.SetInt("N2", PlayerPrefs.GetInt("N2", 2) + 1);
        NumberOfRings += 1;
        PlayerPrefs.SetInt("NR", NumberOfRings);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CallRestartButton()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);

    }
}
