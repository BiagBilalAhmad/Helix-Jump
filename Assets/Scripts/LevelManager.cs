using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public bool IsEndless = false;

    public GameObject Cylinder;


    public static LevelManager Instance;
    public GameObject SliderContainer;


    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if(PlayerPrefs.GetInt("IsLevel",0)==1)
        {
            IsEndless = false;
        }
        else
        {
            IsEndless = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(IsEndless)
        {
            Cylinder.SetActive(false);
            SliderContainer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
