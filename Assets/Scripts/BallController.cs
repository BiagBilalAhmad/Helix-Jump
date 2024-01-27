using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    public float Speed;

    public Rigidbody Rb;

    public GameObject SplitEffect;



    public float bounceStrengthX = 0.5f; // The scale factor for the bounce
    public float bounceDuration = 0.5f;  // The duration of the bounce animation

    private Vector3 originalScale;

    private Rigidbody2D rb;
    public static BallController instance;
    bool ISsoundPlay = false;

    [HideInInspector]
    public bool Motal = false;
    [HideInInspector]
    public int count = 0;
    private void Awake()
    {
        originalScale = transform.localScale;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Rb.velocity = new Vector3(0, Speed, 0)*Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        SoundManager.Instance.PlayBallJump();
        Rb.velocity = new Vector3(0, Speed, 0) * Time.deltaTime;

        Vector3 targetScale = new Vector3(originalScale.x + bounceStrengthX, originalScale.y, originalScale.z);
        transform.DOScale(targetScale, bounceDuration / 2).SetEase(Ease.OutQuad)
            .OnComplete(() => transform.DOScale(originalScale, bounceDuration / 2).SetEase(Ease.InQuad));




        GameObject NewSplit = Instantiate(SplitEffect,
                new Vector3(transform.position.x, collision.transform.position.y + 0.14f, transform.position.z),
                SplitEffect.transform.rotation) as GameObject;


        NewSplit.transform.parent = collision.transform;

        Destroy(NewSplit, .5f);


        if (Motal)
        {
            Destroy(collision.transform.parent);
            count = 0;
            Motal = false;
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {

            SoundManager.Instance.PlayGameOverSound();
            PlayerPrefs.SetInt("TC", PlayerPrefs.GetInt("TC", 0) + CoinsManager.Instance.Coins);
            CoinsManager.Instance.updateTxt();
            Time.timeScale = 0f;
            GameMnanger.Instance.GameOverScreen(false);
            this.enabled = false;
        }


        if (collision.gameObject.CompareTag("Win"))
        {

            if (!ISsoundPlay)
            {
                PlayerPrefs.SetInt("TC", PlayerPrefs.GetInt("TC", 0) + CoinsManager.Instance.Coins);
                CoinsManager.Instance.updateTxt();
                GameMnanger.Instance.GameOverScreen(true);

                SoundManager.Instance.PlayGamewinSound();
                ISsoundPlay = true;
                Time.timeScale = 0f;

            }
            this.enabled = false;
        }
        else
        {
            count = 0;
            Motal = false;
        }
    }

}
