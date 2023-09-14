using Code.BlackCubeSubmodule.DebugTools.BlackCubeLogger;
using Code.BlackCubeSubmodule.Services.LevelBoot;
using Code.BlackCubeSubmodule.Services.UI.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Code.BlackCubeSubmodule.UI.GameScreen
{
    [DisallowMultipleComponent]
    public sealed class GameView : View
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly EcsCustomInject<LevelService> _levelService = default;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _restartButton;

        public override void Init(IEcsSystems systems)
        {
            _pauseButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _disposable.Clear();
            
            _pauseButton.onClick.AddListener(PauseGame);
            _restartButton.onClick.AddListener(ReloadScene);

            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.R))
                .Subscribe(_ => ReloadScene())
                .AddTo(_disposable);
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
    }
}

