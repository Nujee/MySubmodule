namespace Code.BlackCubeSubmodule.ECS.Features.PerformanceStatistics
{
    public sealed class PerformanceStatisticsFeature : Feature
    {
        public override void Init()
        {
            _systems.Add(new s_UpdateFpsCounter());
        }
    }
}