using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBDStarter : MonoBehaviour {

	UnityEngine.TouchScreenKeyboard keyboard;
	VirtualKeyboard vk = new VirtualKeyboard();
	// Use this for initialization
	void Start () {
		keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
		//OpenKeyboard();
	}

	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			CloseKeyboard();
			OpenKeyboard();
		}
		/*if (Input.GetKeyDown("a"))
		{
			CloseKeyboard();
		}*/
	}


	public void OpenKeyboard()
	{
		{       
			vk.ShowTouchKeyboard();
		}
	}
 
	public void CloseKeyboard()
	{
		{       
			vk.HideTouchKeyboard();
		}
	}
}
