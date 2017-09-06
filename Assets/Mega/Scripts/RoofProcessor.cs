using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RoofProcessor : MonoBehaviour
{

	public Material[] materials;

	void DoTransparent()
	{
		StartCoroutine(TransparentCoroutine());
	}

	private IEnumerator TransparentCoroutine()
	{
		foreach (Material material in materials)
		{
			Debug.Log(material.GetFloat("_Mode"));
			if (material.GetFloat("_Mode") == 0f)
			{
				material.SetFloat("_Mode", 2f);
			}
		}

		float time = 0;		
		while(time < 0.3f) {
			float temp = Mathf.Lerp(1, 0, time / 0.3f);
			
			foreach (Material material in materials)
			{
				Color mColor = material.color;
				Color newColor = new Color(mColor.r,mColor.g, mColor.b,temp);
				//Color newColor = new Color(mColor.r,mColor.g, mColor.b,temp);
				material.SetColor("_Color", newColor);
			}
			/*for (int i = 0; i <= line.anchoredPosition.x/180; i++)
			{
				timeButtons[i].SetPressed();
			}
			for (int i = (int)line.anchoredPosition.x/180; i < 9; i++)
			{
				timeButtons[i].SetNormal();
			}*/
			time += Time.deltaTime;
			yield return null;
		}	
		
		foreach (Material material in materials)
		{
			Color mColor = material.color;
			Color newColor = new Color(mColor.r,mColor.g, mColor.b,0);
			//Color newColor = new Color(mColor.r,mColor.g, mColor.b,temp);
			material.SetColor("_Color", newColor);
		}
		yield return null;
	}

	public void DoStandard()
	{
		StartCoroutine(NonTransparentCoroutine());
	}

	
	private IEnumerator NonTransparentCoroutine()
	{
		

		float time = 0;		
		while(time < 0.3f) {
			float temp = Mathf.Lerp(0, 1, time / 0.3f);
			
			foreach (Material material in materials)
			{
				Color mColor = material.color;
				Color newColor = new Color(mColor.r,mColor.g, mColor.b,temp);
				//Color newColor = new Color(mColor.r,mColor.g, mColor.b,temp);
				material.SetColor("_Color", newColor);
			}
			/*for (int i = 0; i <= line.anchoredPosition.x/180; i++)
			{
				timeButtons[i].SetPressed();
			}
			for (int i = (int)line.anchoredPosition.x/180; i < 9; i++)
			{
				timeButtons[i].SetNormal();
			}*/
			time += Time.deltaTime;
			yield return null;
		}	
		
		foreach (Material material in materials)
		{
			Color mColor = material.color;
			Color newColor = new Color(mColor.r,mColor.g, mColor.b,1);
			//Color newColor = new Color(mColor.r,mColor.g, mColor.b,temp);
			material.SetColor("_Color", newColor);
		}
		
		foreach (Material material in materials)
		{
			if (material.GetFloat("_Mode") == 2f)
			{
				material.SetFloat("_Mode", 0f);
			}
		}
		yield return null;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("p"))
		{
			DoTransparent();
		}
		
		if (Input.GetKeyDown("o"))
		{
			DoStandard();
		}
	}
}
