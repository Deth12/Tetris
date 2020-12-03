using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using Tetris.Configs;
using Tetris.Controllers;
using Tetris.Managers;
using Tetris.Constants;
using Tetris.Data;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class ScoreControllerTests : ZenjectUnitTestFixture
    {
        [UnitySetUp]
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("GameScene");
            Container.Bind<ScoreConfig>().FromInstance(Resources.Load<ScoreConfig>(ConstantPaths.SCORE_CONFIG)).AsSingle();
            Container.Bind<PlayerStats>().FromInstance(new PlayerStats()).AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle();
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(10)]
        public void ScoreController_LinesClearRewardTest(int linesAmount)
        {
            var scoreController = Container.Resolve<IScoreController>();
            var scoreConfig = Container.Resolve<ScoreConfig>();
            scoreController.CollectClearedLines(linesAmount);
            var results = scoreController.GetResults();
            Assert.AreEqual(scoreConfig.RowClearReward * linesAmount, results.score);
        }
    }
}
