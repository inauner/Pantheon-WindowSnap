using MelonLoader;
using System;
using System.Runtime.InteropServices;

namespace WindowSnap;

public class ModMain : MelonMod
{
    public override void OnInitializeMelon()
    {
        MelonLogger.Msg("Hello world!");
        EnableWindowSnap();
    }

    public const string ModVersion = "1.0.0";

    private void EnableWindowSnap()
    {
        // Get Unity window handle
        IntPtr hWnd = GetUnityWindowHandle();
        if (hWnd == IntPtr.Zero)
        {
            MelonLogger.Warning("Could not find Unity window handle.");
            return;
        }
        // Set window style to WS_OVERLAPPEDWINDOW
        const int GWL_STYLE = -16;
        const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
        SetWindowLong(hWnd, GWL_STYLE, (IntPtr)WS_OVERLAPPEDWINDOW);
        // Redraw window with new style
        SetWindowPos(hWnd, IntPtr.Zero, 0, 0, 0, 0, 0x0027);
        MelonLogger.Msg("Enabled Windows Snap Assist for game window.");
    }

    private IntPtr GetUnityWindowHandle()
    {
        // Unity window class is usually "UnityWndClass"
        return FindWindow("UnityWndClass", null);
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
}