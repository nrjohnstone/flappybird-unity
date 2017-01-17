namespace Assets.Scripts
{
    public interface IColumnSpawnStrategy
    {
        void Start();
        bool ShouldSpawnColumn();
        void Spawn();
    }
}