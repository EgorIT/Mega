using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSwaper : MonoBehaviour
{

	public RectTransform rt;
	private Vector2 inPosition = new Vector2(1620f, -1140);
	private Vector2 outPosition= new Vector2(-600f, -1140);
	
	// Use this for initialization
	void Start () {
		rt.anchoredPosition = outPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("a"))
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
		}
	}

	public void RollIn()
	{
		StartCoroutine(Rolling(true));
	}
	
	public void RollOut()
	{
		StartCoroutine(Rolling(false));
	}

	public void Reroll()
	{
		StartCoroutine(Rerolling());
	}

	private IEnumerator Rerolling()
	{
		RollOut();
		yield return new WaitForSeconds(0.4f);
		RollIn();
		//yield return null;
	}

	private IEnumerator Rolling (bool rollIn) {
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
	}
}
