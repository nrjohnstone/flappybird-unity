using System;
using Assets.Scripts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UnityEngine;


namespace FlappyBirds.Tests
{
    [TestClass]
    public class BirdControllerTests
    {
        internal class BirdControllerTestDouble : BirdController
        {
            protected override void NotifyBirdDied()
            {                
            }

            public BirdControllerTestDouble(IInput input, IAnimator anim, IRigidbody2D rb2D) : base(input, anim, rb2D)
            {
            }
        }

        private readonly IInput _mockInput;
        private readonly IAnimator _mockAnimator;
        private readonly IRigidbody2D _mockRigidBody2D;

        public BirdControllerTests()
        {
            _mockInput = Substitute.For<IInput>();
            _mockAnimator = Substitute.For<IAnimator>();
            _mockRigidBody2D = Substitute.For<IRigidbody2D>();
        }

        private BirdController CreateSut()
        {
            var sut = new BirdControllerTestDouble(_mockInput, _mockAnimator, _mockRigidBody2D);
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
            sut.upForce = 200f;

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

            sut.isDead.Should().BeTrue();
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
    }
}
