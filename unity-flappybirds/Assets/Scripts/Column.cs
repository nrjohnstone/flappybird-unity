using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class Column : MonoBehaviour {
        private ColumnController _columnController;

        public void Start()
        {
            _columnController = new ColumnController(MessageHub.Instance);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (_columnController == null)
                return;

            _columnController.OnTriggerEnter2D(new Collider2DWrapper(other));
        }
    }
}
