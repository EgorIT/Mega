using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colomn : MonoBehaviour
{
	public Text[] texts;
	public ChangesInfo changesInfo;

	public void Click(string name)
	{
		changesInfo.SetShop(name);
	}
}
