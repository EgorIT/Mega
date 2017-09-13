#pragma strict

import System;
import System.Globalization;

private var i=0;

#if UNITY_EDITOR

	@MenuItem ("Test tools/ScreenShot")
	static function Screen () {
<<<<<<< HEAD
		ScreenCapture.CaptureScreenshot("KingOfDirt_ScreenShot.png",1);
=======
		Application.CaptureScreenshot("KingOfDirt_ScreenShot.png",4);
>>>>>>> 8a8765e166b2ad1676f1cbfdaf00677d68323086
	}

#endif

function Start()
{
	Debug.Log(Application.dataPath);
}

function Update () {
	if(Input.GetKeyDown(KeyCode.LeftShift))
	{
<<<<<<< HEAD
		ScreenCapture.CaptureScreenshot("Screen_"+  i+".png",1);
=======
		Application.CaptureScreenshot("Screen_"+  i+".png",4);
>>>>>>> 8a8765e166b2ad1676f1cbfdaf00677d68323086
		i++;
	}
}

function ScreenShotMobile()
{
	//AndroidCamera.instance.SaveScreenshotToGallery();
}