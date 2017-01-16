using UnityEngine.UI;

namespace Assets.Scripts.UnityAbstractions
{
    internal class TextWrapper : IText
    {
        private readonly Text _instance;

        public TextWrapper(Text instance)
        {
            _instance = instance;
        }

        public string text { get { return _instance.text; } set { _instance.text = value; } }
    }
}