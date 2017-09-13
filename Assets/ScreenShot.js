#pragma strict

import System;
import System.Globalization;

private var i=0;

#if UNITY_EDITOR

	@MenuItem ("Test tools/ScreenShot")
	static function Screen () {
		Application.CaptureScreenshot("KingOfDirt_ScreenShot.png",4);
	}

#endif

function Start()
{
	///Debug.Log(Application.dataPath);
}

function Update () {
	if(Input.GetKeyDown(KeyCode.LeftShift))
	{
		Application.CaptureScreenshot("Screen_"+  i+".png",4);
		i++;
	}
}

function ScreenShotMobile()
{
	//AndroidCamera.instance.SaveScreenshotToGallery();
}