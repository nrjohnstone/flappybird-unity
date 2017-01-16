namespace Assets.Scripts.UnityAbstractions
{
    public interface ISceneManager
    {
        void LoadScene(int buildIndex);
        IScene GetActiveScene();
    }
}