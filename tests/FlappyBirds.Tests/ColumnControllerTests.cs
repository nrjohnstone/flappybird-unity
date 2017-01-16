using Assets.Scripts;
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
        public void Should()
        {
            var sut = CreateSut();

            _mockCollider2D.GetComponent<Bird>().Returns(true);

            sut.OnTriggerEnter2D(_mockCollider2D);
        }
    }
}