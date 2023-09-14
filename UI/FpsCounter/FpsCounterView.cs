using Code.BlackCubeSubmodule.ECS.Features.PerformanceStatistics;
using Code.BlackCubeSubmodule.Services.UI.ScreenService;
using Code.BlackCubeSubmodule.Services.UI.Views;
using Leopotam.EcsLite.Di;
using TMPro;
using UnityEngine;

namespace Code.BlackCubeSubmodule.UI.FpsCounter
{
    [DisallowMultipleComponent]
    public class FpsCounterView : View, IAccessibleToScreenService
    {
        private readonly EcsFilterInject<Inc<c_FpsCounterModel>> _fpsCounterModel = default;

        [SerializeField] public TextMeshProUGUI FpsText;

        protected override void OnOpen()
        {
            foreach (var entity in _fpsCounterModel.Value)
            {
                ref var c_model = ref _fpsCounterModel.Pools.Inc1.Get(entity);
                c_model.Fps.OnChanged += UpdateCounterText;
            }
        }

        protected override void OnClose()
        {
            foreach (var entity in _fpsCounterModel.Value)
            {
                ref var c_model = ref _fpsCounterModel.Pools.Inc1.Get(entity);
                c_model.Fps.OnChanged -= UpdateCounterText;
            }
        }

        private void UpdateCounterText(float newValue)
        {
            FpsText.text = $"{newValue:F2}";
        }
    }
}