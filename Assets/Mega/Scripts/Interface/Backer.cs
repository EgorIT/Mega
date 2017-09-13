using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backer : MonoBehaviour
{

	public Table allShops;
	public Table shopList;
	public Table shop;
	public bool all = false;
	
	public void Back()
	{
		if (all)
		{
			shop.RollOut();
			allShops.RollIn();
		}
		else
		{
			shop.RollOut();
			shopList.RollIn();
			all = false;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
