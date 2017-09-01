using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutImage : MonoBehaviour
{
	public SwitchImages si;
	public int number;

	public void Clicked()
	{
		si.MoveImages(number);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
