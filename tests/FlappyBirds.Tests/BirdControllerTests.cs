using System;
using Assets.Scripts;
using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TinyMessenger;
using UnityEngine;


namespace FlappyBirds.Tests
{
    [TestClass]
    public class BirdControllerTests
    {       
        private readonly IInput _mockInput;
        private readonly IAnimator _mockAnimator;
        private readonly IRigidbody2D _mockRigidBody2D;
        private readonly ITinyMessengerHub _mockMessengerHub;

        public BirdControllerTests()
        {
            _mockInput = Substitute.For<IInput>();
            _mockAnimator = Substitute.For<IAnimator>();
            _mockRigidBody2D = Substitute.For<IRigidbody2D>();
            _mockMessengerHub = Substitute.For<ITinyMessengerHub>();
        }

        private BirdController CreateSut()
        {
            var sut = new BirdController(_mockInput, _mockAnimator, _mockRigidBody2D, _mockMessengerHub);
            return sut;
        }

        private static void MakeBirdDead(BirdController sut)
        {
            sut.OnCollisionEnter2D(new Collision2D());
        }

        [TestMethod]
        public void LeftButtonDown_ShouldAnimateWithFlap()
        {
            var sut = CreateSut();
            _mockInput.IsLeftMouseButtonDown().Returns(true);

            sut.Update();

            _mockAnimator.Received().SetTrigger("Flap");
        }

        [TestMethod]
        public void LeftButtonDown_ShouldAddUpForce()
        {
            var sut = CreateSut();
            _mockInput.IsLeftMouseButtonDown().Returns(true);
            sut.UpForce = 200f;

            sut.Update();

            Vector2 expectedVector = new Vector2(0, 200);
            _mockRigidBody2D.Received().AddForce(expectedVector);
        }

        [TestMethod]
        public void LeftButtonDown_WhenBirdIsDead_ShouldNotAddUpForce()
        {
            var sut = CreateSut();
            _mockInput.IsLeftMouseButtonDown().Returns(true);            
            MakeBirdDead(sut);

            sut.Update();

            _mockRigidBody2D.DidNotReceive().AddForce(Arg.Any<Vector2>());
        }
       
        [TestMethod]
        public void LeftButtonDown_WhenBirdIsDead_ShouldNotAnimateWithFlap()
        {
            var sut = CreateSut();
            _mockInput.IsLeftMouseButtonDown().Returns(true);
            MakeBirdDead(sut);

            sut.Update();

            _mockAnimator.DidNotReceive().SetTrigger("Flap");
        }

        [TestMethod]
        public void WhenBirdCollision_ShouldSetBirdToDead()
        {
            var sut = CreateSut();
            _mockInput.IsLeftMouseButtonDown().Returns(true);
            
            sut.OnCollisionEnter2D(new Collision2D());

            sut.IsDead.Should().BeTrue();
        }

        [TestMethod]
        public void WhenBirdCollision_ShouldAnimateWithDie()
        {
            var sut = CreateSut();
            _mockInput.IsLeftMouseButtonDown().Returns(true);

            sut.OnCollisionEnter2D(new Collision2D());

            _mockAnimator.Received().SetTrigger("Die");
        }

        [TestMethod]
        public void WhenBirdCollision_ShouldSetVelocityToZero()
        {
            var sut = CreateSut();
            _mockInput.IsLeftMouseButtonDown().Returns(true);

            sut.OnCollisionEnter2D(new Collision2D());

            _mockRigidBody2D.Received().velocity = Vector2.zero;
        }

        [TestMethod]
        public void WhenBirdCollision_ShouldPublishBirdDiedMessage()
        {
            var sut = CreateSut();
            _mockInput.IsLeftMouseButtonDown().Returns(true);

            sut.OnCollisionEnter2D(new Collision2D());

            _mockMessengerHub.Received().Publish(Arg.Any<BirdDiedMessage>());
        }
    }
}
