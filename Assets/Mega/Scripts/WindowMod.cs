using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowMod : MonoBehaviour {
    public static Rect screenPosition;
    public GameObject canvas;
    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong (IntPtr hwnd, int _nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    static extern bool SetWindowPos (IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow ();

    // not used rigth now
    //const uint SWP_NOMOVE = 0x2;
    //const uint SWP_NOSIZE = 1;
    //const uint SWP_NOZORDER = 0x4;
    //const uint SWP_HIDEWINDOW = 0x0080;

    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;
    const int WS_BORDER = 1;

    public void Start() {
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            canvas.SetActive(false);
        }
    }

    public static void StartFromController () {
           Debug.Log("ChangeScreenResolution");
        screenPosition.x = 0;
        screenPosition.y = 0;
        screenPosition.width = 3840;//3840 Screen.currentResolution.width; 1920
        screenPosition.height = 2160;//2160 Screen.currentResolution.height; 1080
        SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_BORDER);
        bool result = SetWindowPos(GetForegroundWindow(), 0, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
    }

    public void Set3840_1920() {
        SetSize(3840, 1920);
    }

    public void Set1920_1080 () {
        SetSize(1920, 1080);
    }

    public void Set1024_786 () {
        SetSize(1024, 786);
    }

    public void SetSize(int x, int y) {
        Debug.Log("ChangeScreenResolution");
        screenPosition.x = 0;
        screenPosition.y = 0;
        screenPosition.width = x;//3840 Screen.currentResolution.width; 1920
        screenPosition.height = y;//2160 Screen.currentResolution.height; 1080
        SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_BORDER);
        bool result = SetWindowPos(GetForegroundWindow(), 0, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
        canvas.SetActive(false);
    }
 
}