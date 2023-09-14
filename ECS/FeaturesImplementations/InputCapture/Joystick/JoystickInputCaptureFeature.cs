using Code.BlackCubeSubmodule.ECS.Features;

namespace Code.BlackCubeSubmodule.ECS.FeaturesImplementations.InputCapture.Joystick
{
    public sealed class JoystickInputCaptureFeature : Feature
    {
        public override void Init()
        {
            _systems.Add(new s_CaptureJoystickInput());
        }
    }
}