using System;
using Assets.Scripts;
using Assets.Scripts.UnityAbstractions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace FlappyBirds.Tests
{
    [TestClass]
    public class FixedColumnSpawnerTests
    {
        private Func<IGameObject> _columnFactory;
        private ITime _time;
        private int _columnFactoryCallCount;

        private FixedColumnSpawner CreateSut()
        {
            _columnFactory = () =>
            {
                _columnFactoryCallCount++;
                return Substitute.For<IGameObject>();
            };

            _time = Substitute.For<ITime>();

            var sut = new FixedColumnSpawner(_columnFactory)
            {
                Time = _time
            };
            return sut;
        }

        [TestMethod]
        public void Initialize_ShouldUseFactoryToCreateColumns()
        {
            var sut = CreateSut();

            sut.Initialize();

            _columnFactoryCallCount.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void Initialize_ShouldCreateNumberOfColumnsUsingPoolSize()
        {
            var sut = CreateSut();
            sut.ColumnPoolSize = 10;
            sut.Initialize();

            _columnFactoryCallCount.Should().Be(sut.ColumnPoolSize);
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
        public void Spawn_ShouldUpdatePositionOfCurrentColumn_UsingSpawnPositions()
        {
            var sut = CreateSut();
            sut.SpawnYPosition = 1.5f;
            sut.SpawnXPosition = 11.5f;
            sut.Initialize();

            sut.Spawn();

            sut.GetColumnPosition(0).y.Should().Be(sut.SpawnYPosition);
            sut.GetColumnPosition(0).x.Should().Be(sut.SpawnXPosition);
        }

        [TestMethod]
        public void Spawn_WhenCalledForEachColumnInPool_ShouldUpdateColumnPosition()
        {
            var sut = CreateSut();
            sut.ColumnPoolSize = 2;
            sut.SpawnXPosition = 5f;
            sut.SpawnYPosition = 6f;
            sut.Initialize();

            sut.Spawn();

            // act
            sut.Spawn();

            sut.GetColumnPosition(0).y.Should().Be(sut.SpawnYPosition);
            sut.GetColumnPosition(0).x.Should().Be(sut.SpawnXPosition);
            sut.GetColumnPosition(1).y.Should().Be(sut.SpawnYPosition);
            sut.GetColumnPosition(1).x.Should().Be(sut.SpawnXPosition);
        }

        [TestMethod]
        public void Spawn_WhenLastColumnHasBeenUpdated_ShouldWrapToFirstColumn()
        {
            var sut = CreateSut();
            sut.ColumnPoolSize = 2;
            sut.SpawnYPosition = 5f;
            sut.Initialize();

            sut.Spawn();
            sut.Spawn();

            sut.SpawnYPosition = 6f;

            // act
            sut.Spawn();

            sut.GetColumnPosition(0).y.Should().Be(sut.SpawnYPosition);
        }

        [TestMethod]
        public void ShouldNotBeAbleToSpawnColumn_WithinSpawnRate_AfterSpawningAColumn()
        {
            var sut = CreateSut();
            sut.ColumnPoolSize = 2;
            sut.SpawnYPosition = 5f;
            sut.SpawnRate = 5f;
            sut.Initialize();

            _time.deltaTime.Returns(sut.SpawnRate);

            sut.ShouldSpawnColumn().Should().BeTrue();
            sut.Spawn();
            _time.deltaTime.Returns(0.1f);

            // act
            var shouldSpawnColumn = sut.ShouldSpawnColumn();

            shouldSpawnColumn.Should().BeFalse();
        }
    }
}