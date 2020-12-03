using Tetris.Configs;
using Tetris.Controllers;
using Tetris.Managers;
using UnityEngine;
using Zenject;

public class MyMonoInstaller : MonoInstaller
{
    [SerializeField] private UIManager _uiManager = default;
    [SerializeField] private SpawnManager _spawnManager = default;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<GameController>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<InputController>().AsSingle();
        Container.BindInterfacesAndSelfTo<BlockController>().AsSingle();

        Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle();
        Container.Bind<SpawnManager>().FromInstance(_spawnManager).AsSingle();

        Container.Bind<ScoreConfig>().FromInstance(Resources.Load<ScoreConfig>("Configs/ScoreConfig"));
        Container.Bind<InputConfig>().FromInstance(Resources.Load<InputConfig>("Configs/InputConfig"));
        Container.Bind<BlocksConfig>().FromInstance(Resources.Load<BlocksConfig>("Configs/BlocksConfig"));

        Container.Bind<PlayerStats>().FromInstance(new PlayerStats());
        Container.Bind<ScoreController>().AsSingle();
        Container.Bind<GridManager>().AsSingle();
    }
}