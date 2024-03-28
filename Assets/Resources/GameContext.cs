using Character;
using Level;
using UI.Scripts;
using UnityEngine;
using Zenject;

namespace Resources
{
    public class GameContext : MonoInstaller
    {
        [SerializeField] private MainCharacter _mainCharacterPrefab;
        [SerializeField] private ScreenController _screenController;
        [SerializeField] private LevelLauncher _levelLauncher;
        //[SerializeField] private SoundController _soundController;
        
        public override void InstallBindings()
        {
            BindUI();
            BindPlayer();
            BindLevelLauncher();
        }

        private void BindPlayer()
        {
            Container.Bind<MainCharacter>()
                .FromInstance(_mainCharacterPrefab)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindUI()
        {
            Container.Bind<ScreenController>()
                .FromInstance(_screenController)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindLevelLauncher()
        {
            Container.Bind<LevelLauncher>()
                .FromInstance(_levelLauncher)
                .AsSingle()
                .NonLazy();
        }
    }
}