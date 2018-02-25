using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using UnityEngine;

public class WindowMod : MonoBehaviour {
    public static Rect screenPosition;
    //public GameObject canvas;
    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong (IntPtr hwnd, int _nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    static extern bool SetWindowPos (IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow ();

    public string pathInfo; //C:\Mega.txt

    // not used rigth now
    //const uint SWP_NOMOVE = 0x2;
    //const uint SWP_NOSIZE = 1;
    //const uint SWP_NOZORDER = 0x4;
    //const uint SWP_HIDEWINDOW = 0x0080;

    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;

    public int width;
    public int height;

    public void Start() {
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            //canvas.SetActive(false);
        } else {
            StartCoroutine(ReadVideoInfo());
            //canvas.SetActive(true);
        }
    }

    private IEnumerator ReadVideoInfo () {
        yield return new WaitForSeconds(2f);
        if(!File.Exists(pathInfo)) {
            Debug.LogError("Path error " + pathInfo);
            SetSize(800, 600);
            yield break;
        }
        var pathInfoGlobalStr = File.ReadAllText(pathInfo);
        var xmlGlobal = new XmlDocument();
        xmlGlobal.LoadXml(pathInfoGlobalStr);
        int i = 0;
        foreach(XmlElement node in xmlGlobal.SelectNodes("list/info")) {
            width = Int32.Parse(node.SelectSingleNode("widht").InnerText);
            height = Int32.Parse(node.SelectSingleNode("height").InnerText);
        }
        SetSize(width, height);
    }

    //public static void StartFromController () {
    //    Debug.Log("ChangeScreenResolution");
    //    //screenPosition.x = 0;
    //    //screenPosition.y = 0;
    //    //screenPosition.width = 1920;//3840 Screen.currentResolution.width; 1920
    //    screenPosition.width = 3840;//3840 Screen.currentResolution.width; 1920
    //    //screenPosition.height = 1080;//2160 Screen.currentResolution.height; 1080
    //    screenPosition.height = 2160;//2160 Screen.currentResolution.height; 1080
    //    SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_BORDER);
    //    bool result = SetWindowPos(GetForegroundWindow(), 0, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
    //}

    //public void Set3840_1920() {
    //    SetSize(3840, 1920);
    //}
    //
    //public void Set1920_1080 () {
    //    SetSize(1920, 1080);
    //}
    //
    //public void Set1024_786 () {
    //    SetSize(1024, 786);
    //}

    public void SetSize(int x, int y) {
        Debug.Log("ChangeScreenResolution");
        screenPosition.x = 0;
        screenPosition.y = 0;
        screenPosition.width = x;//3840 Screen.currentResolution.width; 1920
        screenPosition.height = y;//2160 Screen.currentResolution.height; 1080
        SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_BORDER);
        bool result = SetWindowPos(GetForegroundWindow(), 0, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
        //canvas.SetActive(false);
    }
 
}