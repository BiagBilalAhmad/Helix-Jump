using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : MonoBehaviour
{


    public GameObject[] Rings;
    public Rigidbody[] Rb;
    public Renderer[] RingsRenderer;
    public float radius;
    public float force;
    public Material Black;
    public Material Yellow;
    int rand = 0;

    bool ISsoundPlay = false;


   
    private void Awake()
    {

        Randomness();

    }


    public void Randomness()
    {

        rand = Random.Range(0, Rings.Length);
        RingsRenderer[rand].material = Black;
        Rings[rand].tag = "Enemy";



        rand = Random.Range(0, Rings.Length);
        RingsRenderer[rand].material = Black;
        Rings[rand].tag = "Enemy";



        rand = Random.Range(0, Rings.Length);
        RingsRenderer[rand].material = Black;
        Rings[rand].tag = "Enemy";

        Rings[Random.Range(0, Rings.Length)].SetActive(false);
        Rings[Random.Range(0, Rings.Length)].SetActive(false);

    }


    public void ByDefaulty()
    {
        for(int i=0;i<Rings.Length;i++)
        {
            Rings[i].SetActive(true);
            RingsRenderer[i].material = Yellow;
            Rings[i].tag = "Untagged";
        }
    }
    private void Update()
    {
        if (transform.position.y - 0.2f > BallController.instance.transform.position.y)
        {
          


            if(!LevelManager.Instance.IsEndless)
            {
                for (int i = 0; i < Rings.Length; i++)
                {
                    Rb[i].useGravity = true;
                    Rb[i].isKinematic = false;

                    if(!ISsoundPlay)
                    {
                        BallController.instance.count++;
                        if(BallController.instance.count>2)
                        {
                            BallController.instance.Motal = true;
                        }
                        CoinsManager.Instance.AddCoins( Random.Range(2,10));
                        SoundManager.Instance.PlayScoreSound();
                        GameMnanger.Instance.UpdateSlider();
                        ISsoundPlay = true;
                    }
                    Rb[i].AddExplosionForce(force, transform.position, radius);
                  
                    Destroy(this.gameObject, 1f);

                }
            }
            else
            {


                transform.position = new Vector3(transform.position.x, GameMnanger.Instance.yPosition, transform.position.z);
                GameMnanger.Instance.yPosition -= GameMnanger.Instance.MaxDistance;
                CoinsManager.Instance.AddCoins(Random.Range(2, 10));
                SoundManager.Instance.PlayScoreSound();
                ByDefaulty();
                Randomness();
            }
           
        }
    }



}