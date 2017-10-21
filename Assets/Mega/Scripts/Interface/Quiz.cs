using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

	private string[] quiz = new[]
		{
			"Вам удобен проезд к нашему торговому центру?", 
			"Вас устраивает работа сервисных служб: охраны, парковки, уборки?", 
			"Согласны ли Вы с оперативным решением оперативных вопросов с персоналом торгового центра?",
			"Вам нравится новая напольная плитка?",
			"Мы делаем редизайн ТЦ Мегаю Считаете ли Вы, что это поможет привлечь больше посетителей и повысить обороты продаж?",
			"Мы благодарим Вас за помощь!"
		};
	public RectTransform text;
	public GameObject b1;
	public GameObject b2;
	public Swaper swaper;

	public Text content;
	private int curr = 0;
	
	public void OnEnable()
	{
		b1.SetActive(true);
		b2.SetActive(true);
		curr = 0;
		content.text = quiz[0];
		StartCoroutine(MoveText());
	}

	private IEnumerator MoveText()
	{
		float time = 0.5f;

		while (time>0)
		{
			float x = Mathf.Lerp(220, 2080, time / 0.5f);
			text.anchoredPosition = new Vector2(x, text.anchoredPosition.y);
			time -= Time.deltaTime;			
			yield return null;
		}
		text.anchoredPosition = new Vector2(220, text.anchoredPosition.y);
		yield return null;
	}

	public void Anyway()
	{
		StartCoroutine(RemoveText());
	}


	public IEnumerator RemoveText()
	{
		float time = 0.5f;

		while (time>0)
		{
			float x = Mathf.Lerp(-2000, 220, time / 0.5f);
			text.anchoredPosition = new Vector2(x, text.anchoredPosition.y);
			time -= Time.deltaTime;			
			yield return null;
		}

			curr++;
			content.text = quiz[curr];
			StartCoroutine(MoveText());
		if (curr == quiz.Length-1)
		{
			b1.SetActive(false);
			b2.SetActive(false);
			swaper.DelayedSwap();
		}
		yield return null;
	}

	// Use this for initialization
	void Awake ()
	{
		//content = text.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown("m"))
		{
			StartCoroutine(MoveText());
		}*/
	}
}
