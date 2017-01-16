using System;
using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        public static Game instance;
        public GameObject gameOverText;
        public Text scoreText;
        public bool gameOver { get { return _gameController.gameOver; } }
        public float scrollSpeed = -1.5f;
        private GameController _gameController;

        void Awake () {
            if (instance == null)
            {
                instance = this;
                _gameController = new GameController(new TextWrapper(scoreText), 
                    new GameObjectWrapper(gameOverText), MessageHub.Instance);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (gameOver && Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
