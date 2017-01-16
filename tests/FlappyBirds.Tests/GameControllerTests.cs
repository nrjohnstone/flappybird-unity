using Assets.Scripts;
using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TinyMessenger;
using UnityEngine.UI;

namespace FlappyBirds.Tests
{
    [TestClass]
    public class GameControllerTests
    {
        private IText _scoreText;
        private IGameObject _gameOverText;
        private ITinyMessengerHub _messenger;

        private GameController CreateSut()
        {
            _scoreText = Substitute.For<IText>();
            _gameOverText = Substitute.For<IGameObject>();
            _messenger = new TinyMessengerHub();
            var sut = new GameController(_scoreText, _gameOverText, _messenger);
            return sut;
        }

        [TestMethod]
        public void BirdScored_ShouldIncreaseScoreByOne()
        {
            var sut = CreateSut();

            sut.BirdScored();

            sut.score.Should().Be(1);
        }

        [TestMethod]
        public void BirdScored_ShouldUpdateScoreText()
        {
            var sut = CreateSut();

            sut.BirdScored();

            _scoreText.Received().text = "Score: 1";
        }

        [TestMethod]
        public void BirdDied_ShouldSetGameOverTextToVisible()
        {
            var sut = CreateSut();

            sut.BirdDied();

            _gameOverText.Received().SetActive(true);
        }

        [TestMethod]
        public void BirdDied_ShouldSetGameOver()
        {
            var sut = CreateSut();

            sut.BirdDied();

            sut.gameOver.Should().Be(true);
        }

        [TestMethod]
        public void Ctor_ShouldSubscribeToBirdDiedMessage()
        {
            var sut = CreateSut();

            _messenger.Publish(new BirdDiedMessage());

            sut.gameOver.Should().Be(true);
        }

        [TestMethod]
        public void Ctor_ShouldSubscribeToBirdScoredMessage()
        {
            var sut = CreateSut();

            _messenger.Publish(new BirdScoredMessage());

            sut.score.Should().Be(1);
        }
    }
}