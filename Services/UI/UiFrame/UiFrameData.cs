using UnityEngine;

namespace Code.BlackCubeSubmodule.Services.UI.UiFrame
{
    public sealed class UiFrameData
    {
        public readonly UnityEngine.Camera UiCamera;
        public readonly Transform Background;
        public readonly Transform Windows;
        public readonly Transform Overlay;

        public UiFrameData(UnityEngine.Camera uiCamera, Transform background, Transform windows, Transform overlay)
        {
            UiCamera = uiCamera;
            Background = background;
            Windows = windows;
            Overlay = overlay;
        }
    }
}