#pragma strict

import System;
import System.Globalization;

private var i=0;

#if UNITY_EDITOR

	@MenuItem ("Test tools/ScreenShot")
	static function Screen () {
		ScreenCapture.CaptureScreenshot("KingOfDirt_ScreenShot.png",1);
	}

#endif

function Start()
{
	Debug.Log(Application.dataPath);
}

function Update () {
	if(Input.GetKeyDown(KeyCode.LeftShift))
	{
		ScreenCapture.CaptureScreenshot("Screen_"+  i+".png",1);
		i++;
	}
}

function ScreenShotMobile()
{
	//AndroidCamera.instance.SaveScreenshotToGallery();
}