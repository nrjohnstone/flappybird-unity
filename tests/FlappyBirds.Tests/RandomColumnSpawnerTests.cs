using System;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.UnityAbstractions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace FlappyBirds.Tests
{
    [TestClass]
    public class RandomColumnSpawnerTests
    {
        private Func<IGameObject> _columnFactory;
        private ITime _time;
        private IRandom _random;

        private RandomColumnSpawner CreateSut()
        {
            _time = Substitute.For<ITime>();
            _random = Substitute.For<IRandom>();
            _columnFactory = Substitute.For<Func<IGameObject>>();

            var sut = new RandomColumnSpawner(
                () => _columnFactory())
            {
                Time = _time,
                Random = _random
            };

            return sut;
        }

        [TestMethod]
        public void Initialize_ShouldUseFactoryToCreateColumns()
        {
            var sut = CreateSut();

            sut.Initialize();

            _columnFactory.Received().Invoke();
        }

        [TestMethod]
        public void Initialize_ShouldCreateNumberOfColumnsUsingPoolSize()
        {
            var sut = CreateSut();
            sut.ColumnPoolSize = 10;
            sut.Initialize();

            _columnFactory.Received(sut.ColumnPoolSize).Invoke();
        }

        [TestMethod]
        public void ShouldSpawnColumn_WhenTimeSinceLastColumnIsLessThanRate_ShouldReturnFalse()
        {
            var sut = CreateSut();
            sut.SpawnRate = 10f;
            _time.deltaTime.Returns(sut.SpawnRate - 0.1f);
            var shouldSpawnColumn = sut.ShouldSpawnColumn();

            shouldSpawnColumn.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldSpawnColumn_WhenTimeSinceLastColumnIsEqualToRate_ShouldReturnTrue()
        {
            var sut = CreateSut();
            sut.SpawnRate = 10f;
            _time.deltaTime.Returns(sut.SpawnRate);
            var shouldSpawnColumn = sut.ShouldSpawnColumn();

            shouldSpawnColumn.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldSpawnColumn_WhenTimeSinceLastColumnIsGreaterThanRate_ShouldReturnTrue()
        {
            var sut = CreateSut();
            sut.SpawnRate = 10f;
            _time.deltaTime.Returns(sut.SpawnRate + 0.1f);
            var shouldSpawnColumn = sut.ShouldSpawnColumn();

            shouldSpawnColumn.Should().BeTrue();
        }

        [TestMethod]
        public void Spawn_ShouldUpdateYPositionOfCurrentColumn_UsingRandomValue()
        {
            var sut = CreateSut();
            sut.Initialize();

            float randomValue = 5f;
            _random.Range(Arg.Any<float>(), Arg.Any<float>()).Returns(randomValue);
            sut.Spawn();

            sut.GetColumnPosition(0).y.Should().Be(randomValue);
        }

        [TestMethod]
        public void Spawn_ShouldUpdateXPositionOfCurrentColumn_ToOffscreenXValue()
        {
            var sut = CreateSut();
            sut.Initialize();
            
            sut.Spawn();

            sut.GetColumnPosition(0).x.Should().Be(sut.SpawnXPosition);
        }

        [TestMethod]
        public void Spawn_ShouldGetRandomValueBetweenMinAndMax()
        {
            var sut = CreateSut();
            sut.Initialize();

            sut.Spawn();

            _random.Received().Range(sut.ColumnMin, sut.ColumnMax);
        }

        [TestMethod]
        public void Spawn_WhenCalledForEachColumnInPool_ShouldUpdateColumnPosition()
        {
            var sut = CreateSut();
            sut.ColumnPoolSize = 2;
            sut.Initialize();

            float randomValue = 5f;
            _random.Range(Arg.Any<float>(), Arg.Any<float>()).Returns(randomValue);
            sut.Spawn();

            // act
            sut.Spawn();

            sut.GetColumnPosition(0).y.Should().Be(randomValue);
            sut.GetColumnPosition(1).y.Should().Be(randomValue);
        }

        [TestMethod]
        public void Spawn_WhenLastColumnHasBeenUpdated_ShouldWrapToFirstColumn()
        {
            var sut = CreateSut();
            sut.ColumnPoolSize = 2;
            sut.Initialize();

            _random.Range(Arg.Any<float>(), Arg.Any<float>()).Returns(5f);
            sut.Spawn();
            sut.Spawn();

            float firstColumnExpectedRandomValue = 10f;
            _random.Range(Arg.Any<float>(), Arg.Any<float>()).Returns(firstColumnExpectedRandomValue);

            // act
            sut.Spawn();

            sut.GetColumnPosition(0).y.Should().Be(firstColumnExpectedRandomValue);
        }
    }
}