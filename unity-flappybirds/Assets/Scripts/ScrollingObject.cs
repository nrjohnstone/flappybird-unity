using Assets.Scripts.Messaging;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScrollingObject : MonoBehaviour {
        
        public void Start ()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _rb2D.velocity = new Vector2(Game.instance.scrollSpeed, 0);
            MessageHub.Instance.Subscribe<GameOverMessage>(m => GameOver());
        }

        private void GameOver()
        {
            _gameOver = true;
        }

        public void Update () {
            if (_gameOver)
            {
                _rb2D.velocity = Vector2.zero;
            }
        }

        private Rigidbody2D _rb2D;
        private bool _gameOver;

    }
}
