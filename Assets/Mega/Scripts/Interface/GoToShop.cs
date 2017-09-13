using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoToShop : MonoBehaviour
{

	private AllCaps allCaps;
	private Image image;
	
	public void PushButton()
	{
		ShopCap shopCap = allCaps.allCaps.First(x => x.GetComponent<ShopCap>().name == image.name);
		if (shopCap != null)
		{
			shopCap.MoveCamera();
		}
	}

	// Use this for initialization
	void Start ()
	{
		allCaps = GameObject.FindObjectOfType<AllCaps>();
		image = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
