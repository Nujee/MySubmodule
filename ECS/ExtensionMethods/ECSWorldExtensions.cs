using JetBrains.Annotations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace Code.MySubmodule.ECS.ExtensionMethods
{
    public static class ECSWorldExtensions
    {
        [PublicAPI]
        public static void TurnSystemGroupOn(this EcsWorld world, string groupName)
        {
            ChangeSystemGroupState(world, groupName, true);
        }
        
        [PublicAPI]
        public static void TurnSystemGroupOff(this EcsWorld world, string groupName)
        {
            ChangeSystemGroupState(world, groupName, false);
        }

        private static void ChangeSystemGroupState(EcsWorld world, string groupName, bool newState)
        {
            var entity = world.NewEntity();
            ref var eventGroup = ref world.GetPool<EcsGroupSystemState>().Add(entity);
            eventGroup.Name = groupName;
            eventGroup.State = newState;
        }
    }
}