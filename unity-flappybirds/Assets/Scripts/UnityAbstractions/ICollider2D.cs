namespace Assets.Scripts.UnityAbstractions
{
    public interface ICollider2D
    {
        T GetComponent<T>();
        string tag { get; set; }
    }
}