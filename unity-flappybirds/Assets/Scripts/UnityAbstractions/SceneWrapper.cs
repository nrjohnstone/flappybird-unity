using UnityEngine.SceneManagement;

namespace Assets.Scripts.UnityAbstractions
{
    public class SceneWrapper : IScene
    {
        private readonly Scene _instance;

        public SceneWrapper(Scene instance)
        {
            _instance = instance;
        }

        public int buildIndex {
            get { return _instance.buildIndex; }
        }
    }
}