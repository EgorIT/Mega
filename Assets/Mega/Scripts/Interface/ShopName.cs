using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopName : MonoBehaviour
{

	public Colomn colomn;

	public void Click()
	{
		
		colomn.Click(gameObject.GetComponent<Text>().text);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
