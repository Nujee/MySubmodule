using Code.BlackCubeSubmodule.ECS.OnInitTagActionsFeature;

namespace Code.BlackCubeSubmodule.ECS.Features.OnInitTagActions
{
    public sealed class OnInitTagActionsFeature : Feature
    {
        public override void Init()
        {
            _systems.Add(new i_DisableRenderersOnStartInitSystem());
            _systems.Add(new i_DisableSelfAndChildrenRenderersOnStart());
            _systems.Add(new i_DetachChildrenOnStartInitSystem());
        }
    }
}