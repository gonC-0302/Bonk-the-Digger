using UnityEngine;

public class AspectRatioMaintainer : MonoBehaviour
{
    private int lastWidth;
    private int lastHeight;

    void Start()
    {
        UpdateResolution();
    }

    void Update()
    {
        // ウィンドウサイズが変更されたら解像度を更新
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            UpdateResolution();
        }
    }

    void UpdateResolution()
    {
        lastWidth = Screen.width;
        lastHeight = Screen.height;

        Screen.SetResolution(lastWidth, lastHeight, false);
    }
}
