using UnityEngine.SceneManagement;

namespace Assets.Scripts.UnityAbstractions
{
    public class AmbientSceneManager : ISceneManager
    {
        public void LoadScene(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }

        public IScene GetActiveScene()
        {
            return new SceneWrapper(SceneManager.GetActiveScene());
        }
    }
}