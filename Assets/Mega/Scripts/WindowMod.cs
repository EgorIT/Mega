﻿using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowMod : MonoBehaviour {
    public static Rect screenPosition;

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

    public static void StartFromController () {
       
        screenPosition.x = 0;
        screenPosition.y = 0;
        screenPosition.width = Screen.currentResolution.width;
        screenPosition.height = Screen.currentResolution.height;
        SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_BORDER);
        bool result = SetWindowPos(GetForegroundWindow(), 0, (int)screenPosition.x, (int)screenPosition.y, (int)screenPosition.width, (int)screenPosition.height, SWP_SHOWWINDOW);
    }

}