using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllCaps : MonoBehaviour {

	public List<ShopCap> allCaps = new List<ShopCap>();
	// Use this for initialization
	void Start ()
	{
		allCaps = GameObject.FindObjectsOfType<ShopCap>().ToList();
	}

	public void Refresh()
	{
		allCaps.ForEach(x=>x.gameObject.SetActive(false));
	}

	public void Activate(string name)
	{
		allCaps.Where(x =>
		{
			if (x.name == name)
				x.gameObject.SetActive(true);
			return true;
		});
	}
}
