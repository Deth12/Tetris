using Tetris.Configs;
using Tetris.Controllers;
using Tetris.Managers;
using UnityEngine;
using Zenject;
using Tetris.Constants;

namespace Tetris.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private UIManager _uiManager = default;
        [SerializeField] private SpawnManager _spawnManager = default;
    
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<InputController>().AsSingle();
            Container.BindInterfacesAndSelfTo<BlockController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle();

            Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle();
            Container.Bind<SpawnManager>().FromInstance(_spawnManager).AsSingle();

            Container.Bind<ScoreConfig>().FromInstance(Resources.Load<ScoreConfig>(ConstantPaths.SCORE_CONFIG)).AsSingle();
            Container.Bind<InputConfig>().FromInstance(Resources.Load<InputConfig>(ConstantPaths.INPUT_CONFIG)).AsSingle();
            Container.Bind<BlocksConfig>().FromInstance(Resources.Load<BlocksConfig>(ConstantPaths.BLOCKS_CONFIG)).AsSingle();

            Container.Bind<PlayerStats>().FromInstance(new PlayerStats()).AsSingle();
            Container.Bind<GridController>().AsSingle();
        }
    }
}
