using TinyMessenger;
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
            _columnController.OnTriggerEnter2D(new Collider2DWrapper(other));
        }
    }

    public class Collider2DWrapper : ICollider2D
    {
        private readonly Collider2D _instance;

        public Collider2DWrapper(Collider2D instance)
        {
            _instance = instance;
        }

        public T GetComponent<T>()
        {
            return _instance.GetComponent<T>();
        }
    }

    public class BirdScoredMessage : ITinyMessage
    {
        public object Sender { get; private set; }
    }
}
