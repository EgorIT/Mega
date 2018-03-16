using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descaler : MonoBehaviour
{

	public RectTransform fullscreen;
	
	public void ScaleFullscreen()
	{
		
		//fullscreen.GetComponent<Image>().sprite = sprites[i].sprite;
		StartCoroutine(ScaleUpFullscreen());
	}

	private IEnumerator ScaleUpFullscreen()
	{
		fullscreen.localScale = Vector2.one;
		float time = 0.2f;
		while(time > 0) {
			//content.sizeDelta = new Vector2(2028-minus, 362);
			fullscreen.localScale = Vector2.Lerp( Vector2.zero, Vector2.one,time / .5f);
			time -= Time.deltaTime;
			yield return null;
		}
		fullscreen.localScale = Vector2.zero;
		yield return null;
		fullscreen.gameObject.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
