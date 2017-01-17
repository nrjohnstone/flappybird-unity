namespace Assets.Scripts
{
    public interface IColumnSpawnStrategy
    {
        void Initialize();
        bool ShouldSpawnColumn();
        void Spawn();
    }
}