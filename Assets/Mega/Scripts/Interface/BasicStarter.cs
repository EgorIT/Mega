using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStarter : MonoBehaviour
{

	public Table table;
	
	private void OnEnable()
	{
		table.RollIn();
	}
}
