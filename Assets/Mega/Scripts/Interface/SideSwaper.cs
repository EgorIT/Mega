using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class SideSwaper : MonoBehaviour
{

	public RectTransform rt;
	private Vector2 inPosition = new Vector2(1620f, -1140);
	private Vector2 outPosition= new Vector2(-600f, -1140);
	private bool letSwipe = true;
	private bool readyToRoll = true;
	private int bufferBlink=-1;
	public Text text;

	private string[] terms = new[]
	{
		"С 9-го апреля 2018г. до 10-го июня 2018г. этот участок дороги будет перекрыт из-за ремонтных работ",
		"С 1-го июня 2018г. до 12-го августа 2018г. этот участок дороги будет перекрыт из-за ремонтных работ",
		"С 1-го июля 2018г. до 30-го июля 2018г. этот участок дороги будет перекрыт из-за ремонтных работ",
		"С 14-го мая 2018г. до 1-го августа 2018г. этот участок дороги будет перекрыт из-за ремонтных работ"
	};
	
	
	// Use this for initialization
	void Start () {
		rt.anchoredPosition = outPosition;
		MainLogic.inst.actionSelectZone += Process;
	}

	private void OnDestroy()
	{
		MainLogic.inst.actionSelectZone -= Process;
	}

	public void Process(int value)
	{
		//Debug.Log(value);
		if (letSwipe)
		{
			if (bufferBlink != value - 20)
			{
				bufferBlink = value - 20;
				if (readyToRoll)
				{
					RollIn();
				}
				else
				{
					Reroll();
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown("a"))
		{
			RollIn();
		}
		
		if (Input.GetKeyDown("s"))
		{
			RollOut();
		}
		
		if (Input.GetKeyDown("d"))
		{
			Reroll();
		}*/
	}

	public void RollIn()
	{
		text.text = terms[bufferBlink];
		StartCoroutine(Rolling(true));
	}
	
	public void RollOut()
	{
		if (readyToRoll==false)
		StartCoroutine(Rolling(false));
	}

	public void Reroll()
	{
		StartCoroutine(Rerolling());
	}

	private IEnumerator Rerolling()
	{
		letSwipe = false;
		readyToRoll = false;
		bool rollIn = false;
		float time = 0;
		//Debug.Log(rt.name);
		//Vector3 startPosition = rt.anchoredPosition;

		
		while(time < 0.3f) {
			if (rollIn) {
				rt.anchoredPosition = Vector3.Lerp(outPosition, inPosition, time / 0.3f);
				//InterfaceController.inst.upperPanelRectTransform.anchoredPosition = rt.anchoredPosition;
			} else {
				rt.anchoredPosition = Vector3.Lerp(inPosition, outPosition, time / 0.3f);
                
			}
			time += Time.deltaTime;
			yield return null;
		}
		
		if(rollIn) {
			rt.anchoredPosition = inPosition;
			//InterfaceController.inst.upperPanelRectTransform.anchoredPosition = rt.anchoredPosition;
			//Countdown();
		} else {
			rt.anchoredPosition = outPosition;
            
		}
      	yield return null;

		text.text = terms[bufferBlink];
		rollIn = true;
		time = 0;
		//Debug.Log(rt.name);
		//Vector3 startPosition = rt.anchoredPosition;

		readyToRoll = false;
		while(time < 0.3f) {
			if (rollIn) {
				rt.anchoredPosition = Vector3.Lerp(outPosition, inPosition, time / 0.3f);
				//InterfaceController.inst.upperPanelRectTransform.anchoredPosition = rt.anchoredPosition;
			} else {
				rt.anchoredPosition = Vector3.Lerp(inPosition, outPosition, time / 0.3f);
                
			}
			time += Time.deltaTime;
			yield return null;
		}
		if(rollIn) {
			rt.anchoredPosition = inPosition;
			//InterfaceController.inst.upperPanelRectTransform.anchoredPosition = rt.anchoredPosition;
			//Countdown();
		} else {
			rt.anchoredPosition = outPosition;
		}
      
		letSwipe = true;
		yield return null;
		
		
		/*RollOut();
		yield return new WaitForSeconds(0.4f);
		RollIn();*/
		//yield return null;
	}

	private IEnumerator Rolling (bool rollIn) {
		letSwipe = false;
		float time = 0;
		//Debug.Log(rt.name);
		//Vector3 startPosition = rt.anchoredPosition;

		readyToRoll = false;
		while(time < 0.3f) {
			if (rollIn) {
				rt.anchoredPosition = Vector3.Lerp(outPosition, inPosition, time / 0.3f);
				//InterfaceController.inst.upperPanelRectTransform.anchoredPosition = rt.anchoredPosition;
			} else {
				rt.anchoredPosition = Vector3.Lerp(inPosition, outPosition, time / 0.3f);
                
			}
			time += Time.deltaTime;
			yield return null;
		}
		if(rollIn) {
			rt.anchoredPosition = inPosition;
			//InterfaceController.inst.upperPanelRectTransform.anchoredPosition = rt.anchoredPosition;
			//Countdown();
		} else {
			rt.anchoredPosition = outPosition;
			readyToRoll = true;
			bufferBlink = -1;
		}
		letSwipe = true;
		yield return null;
		
	}
}
