using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swaper : MonoBehaviour
{

	public Table target;
	public Table current;
	
	public void Swap()
	{
		current.RollOut();
		target.RollIn();
	}
}
