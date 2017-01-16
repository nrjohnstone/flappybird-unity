using Assets.Scripts;
using Assets.Scripts.UnityAbstractions;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnityEngine.UI;

namespace FlappyBirds.Tests
{
    [TestClass]
    public class GameControllerTests
    {
        private IText _scoreText;

        private GameController CreateSut()
        {
            _scoreText = Substitute.For<IText>();
            var sut = new GameController(_scoreText);
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
    }
}