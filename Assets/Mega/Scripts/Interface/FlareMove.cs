using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FlareMove : MonoBehaviour {

	
	private RectTransform rt;
	private Vector3 outPosition = new Vector3(-2300, 0, 0);
	private Vector3 inPosition = new Vector3(2300, 0, 0);
	
	void Start () {
		rt = gameObject.GetComponent<RectTransform>();
		rt.anchoredPosition = outPosition;
	}
	
	public void RollIn () {
		StartCoroutine(Rolling(true));
	}
    
	private IEnumerator Rolling (bool rollIn) {
		float time = 0;
		float timeNeed = .7f;
		Vector3 startPosition = rt.anchoredPosition;

		while(time < timeNeed) {
			if(rollIn)
				rt.anchoredPosition = Vector3.Slerp(outPosition, inPosition, time / timeNeed);
			else {
				rt.anchoredPosition = Vector3.Slerp(inPosition, outPosition, time / timeNeed);
			}
			time += Time.deltaTime;
			yield return null;
		}
		if(rollIn) {
			rt.anchoredPosition = inPosition;
		} else {
			rt.anchoredPosition = outPosition;
		}
		yield return null;
	}
	
	void Update () {
		if(Input.GetKeyDown("z")) {
			RollIn();
		}
	}
}
