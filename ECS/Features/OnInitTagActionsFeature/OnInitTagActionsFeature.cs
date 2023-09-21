using Leopotam.EcsLite;

namespace Code.MySubmodule.ECS.Features.OnInitTagActionsFeature
{
    public sealed class OnInitTagActionsFeature : IFeature
    {
        public void Init(EcsSystems systems)
        {
            systems.Add(new i_DisableRenderersOnStartInitSystem());
            systems.Add(new i_DisableSelfAndChildrenRenderersOnStart());
            systems.Add(new i_DetachChildrenOnStartInitSystem());
        }
    }
}