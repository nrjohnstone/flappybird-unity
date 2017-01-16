using System.Runtime.Serialization;
using Assets.Scripts;
using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TinyMessenger;
using UnityEngine;

namespace FlappyBirds.Tests
{
    [TestClass]
    public class ColumnControllerTests
    {
        private ITinyMessengerHub _mockMessengerHub;
        private ICollider2D _mockCollider2D;

        private ColumnController CreateSut()
        {
            _mockMessengerHub = Substitute.For<ITinyMessengerHub>();
            _mockCollider2D = Substitute.For<ICollider2D>();
            return new ColumnController(_mockMessengerHub);
        }

        [TestMethod]
        public void WhenColumnScoringCollider_CollidesWithBird_ShouldPublishMessageBirdScored()
        {
            var sut = CreateSut();
            _mockCollider2D.tag.Returns("Player");

            sut.OnTriggerEnter2D(_mockCollider2D);

            _mockMessengerHub.Received().Publish(Arg.Any<BirdScoredMessage>());
        }

        [TestMethod]
        public void WhenColumnScoringCollider_CollidesWithNonBird_ShouldNotPublishAnyMessage()
        {
            var sut = CreateSut();
            _mockCollider2D.tag.Returns("");
            
            sut.OnTriggerEnter2D(_mockCollider2D);

            _mockMessengerHub.DidNotReceive().Publish(Arg.Any<BirdScoredMessage>());
        }
    }
}