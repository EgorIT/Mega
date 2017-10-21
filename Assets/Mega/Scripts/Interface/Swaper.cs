using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swaper : MonoBehaviour
{

	public Table target;
	public Table current;

	public void DelayedSwap()
	{
		StartCoroutine(Delayer());
	}

	private IEnumerator Delayer()
	{
		yield return new WaitForSeconds(3f);
		Swap();
	}

	public void Swap()
	{
		current.RollOut();
		target.RollIn();
	}
}
