using Code.MySubmodule.Services.LevelBoot;
using Code.MySubmodule.Services.UI.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MySubmodule.Services.UI.Screens.ConcreteScreens
{
    [DisallowMultipleComponent]
    public sealed class GameScreen : View
    {
        private readonly EcsCustomInject<LevelService> _levelService = default;
        
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _restartButton;

        public override void Init(IEcsSystems systems)
        {
            _pauseButton.onClick.AddListener(() => PauseGame());
            _restartButton.onClick.AddListener(() => ReloadScene());

            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.R))
                .Subscribe(_ => ReloadScene())
                .AddTo(this);
        }

        private void PauseGame()
        {
            Time.timeScale = Time.timeScale > 0.5f ? 0f : 1f;
        }

        private void ReloadScene()
        {
            _levelService.Value.ReloadCurrentScene();
            Time.timeScale = 1f;
        }

        public void OnDestroy()
        {
            if (!_restartButton) return;
            _restartButton.onClick.RemoveAllListeners();
        }
    }
}

