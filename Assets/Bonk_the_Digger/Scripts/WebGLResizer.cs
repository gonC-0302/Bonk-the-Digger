using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLResizer : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SetCanvasSize();

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SetCanvasSize(); // WebGLで実行時にJavaScriptの関数を呼び出す
#endif
    }

    public void UpdateResolution(int width, int height)
    {
        Screen.SetResolution(width, height, false);
    }
}