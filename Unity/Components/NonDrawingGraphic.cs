using UnityEngine.UI;

namespace Code.BlackCubeSubmodule.Unity.Components
{
    /// <summary>
    /// A concrete subclass of the Unity UI `Graphic` class that just skips drawing.
    /// </summary>
    public class NonDrawingGraphic : Graphic
    {
        public override void SetMaterialDirty() { }
        public override void SetVerticesDirty() { }
    }
}