using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class CoinsManager : MonoBehaviour
{
	//References
	[Header ("UI references")]
	[SerializeField] TMP_Text coinUIText;
	[SerializeField] GameObject animatedCoinPrefab;
	[SerializeField] Transform target;

	[Space]
	[Header ("Available coins : (coins to pool)")]
	[SerializeField] int maxCoins;
	Queue<GameObject> coinsQueue = new Queue<GameObject> ();


	[Space]
	[Header ("Animation settings")]
	[SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range (0.9f, 2f)] float maxAnimDuration;

	[SerializeField] Ease easeType;
	[SerializeField] float spread;

	Vector3 targetPosition;


	private int _c = 0;

	public static CoinsManager Instance;
	public float XOffset;
	int Hscore=0;

	public TMP_Text HighScoreTxt, TotalCoinsTxt;
	int TotalCoins=0;
	[SerializeField] ParticleSystem Particle;
	public int Coins {
		get{ return _c; }
		set {
			_c = value;
			//update UI text whenever "Coins" variable is changed
			coinUIText.text = Coins.ToString ();
			
			//Debug.Log("Total Coins" + PlayerPrefs.GetInt("TC", 0)+ " current coins "+Coins);
			Hscore = PlayerPrefs.GetInt("HS", 0);
			if(Coins>=Hscore)
            {
				PlayerPrefs.SetInt("HS", Coins);
            }

			HighScoreTxt.text = "High Score : " + PlayerPrefs.GetInt("HS", 0);
			TotalCoinsTxt.text = "Total Coins : " + PlayerPrefs.GetInt("TC", 0);
		}
	}


	public void updateTxt()
    {
		HighScoreTxt.text = "High Score : " + PlayerPrefs.GetInt("HS", 0);
		TotalCoinsTxt.text = "Total Coins : " + PlayerPrefs.GetInt("TC", 0);
	}
	void Awake()
	{
		Hscore = PlayerPrefs.GetInt("HS", 0);
		TotalCoins = PlayerPrefs.GetInt("TC", 0);
		HighScoreTxt.text = "High Score : " + PlayerPrefs.GetInt("HS", 0);
		TotalCoinsTxt.text = "Total Coins : " + PlayerPrefs.GetInt("TC", 0);
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
		targetPosition = new Vector3(target.position.x + XOffset, target.position.y, target.position.z);

		//prepare pool
		PrepareCoins();
	}

	void PrepareCoins ()
	{
		GameObject coin;
		for (int i = 0; i < maxCoins; i++) {
			coin = Instantiate (animatedCoinPrefab);
			coin.transform.SetParent(transform);
			coin.SetActive (false);
			coinsQueue.Enqueue (coin);
		}
	}

	void Animate ( int amount)
	{
		for (int i = 0; i < amount; i++) {
			//check if there's coins in the pool
			if (coinsQueue.Count > 0) {
				//extract a coin from the pool
				GameObject coin = coinsQueue.Dequeue ();
				coin.SetActive (true);

				//move coin to the collected coin pos
				coin.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -263,0);

				//animate coin to target position
				float duration = Random.Range (minAnimDuration, maxAnimDuration);
				coin.transform.DOMove (targetPosition, duration)
				.SetEase (easeType)
				.OnComplete (() => {
					//executes whenever coin reach target position
					coin.SetActive (false);
					coinsQueue.Enqueue (coin);

					Coins++;
				});
			}
		}
	}

	public void AddCoins (int amount)
	{
		Particle.Play();
		Animate (amount);
	}
}
