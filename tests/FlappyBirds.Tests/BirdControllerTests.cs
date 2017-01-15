using System;
using Assets.Scripts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;


namespace FlappyBirds.Tests
{
    [TestClass]
    public class BirdControllerTests
    {
        private readonly IInput _mockInput;
        private readonly IAnimator _mockAnimator;
        private readonly IRigidbody2D _mockRigidBody2D;

        public BirdControllerTests()
        {
            _mockInput = Substitute.For<IInput>();
            _mockAnimator = Substitute.For<IAnimator>();
            _mockRigidBody2D = Substitute.For<IRigidbody2D>();
        }

        [TestMethod]
        public void LeftButtonDown_ShouldMakeBirdFlap()
        {
            BirdController sut = CreateSut();
            _mockInput.IsLeftMouseButtonDown().Returns(true);

            sut.Update();

            _mockAnimator.Received().SetTrigger("Flap");
        }

        private BirdController CreateSut()
        {
            var sut = new BirdController(_mockInput, _mockAnimator, _mockRigidBody2D);
            return sut;
        }
    }
}
