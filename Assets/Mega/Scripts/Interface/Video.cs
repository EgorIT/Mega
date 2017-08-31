using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{

	public MeshRenderer mr;
	public VideoPlayer vp;
	// Use this for initialization
	void Start () {
		//vp.clip.	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown("q"))
		{
			StartCoroutine(Fade());
		}
	}

	private IEnumerator Fade()
	{
	float time = 0;
		Color ourColor = new Color(1,1,1,1);
		while(time < 1f) {
			ourColor.a =Mathf.Lerp(1, 0, time / 1f);
			mr.material.color = ourColor;
			time += Time.deltaTime;
			yield return null;
		}
		mr.material.color = new Color(1,1,1,0);
		vp.Stop();
		yield return null;
	}
}
