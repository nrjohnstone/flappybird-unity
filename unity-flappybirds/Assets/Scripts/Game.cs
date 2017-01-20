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
        public float scrollSpeed = -1.5f;
        private GameController _gameController;

        void Awake () {
            if (instance == null)
            {
                instance = this;
                _gameController = new GameController(new TextWrapper(scoreText), 
                    new GameObjectWrapper(gameOverText), MessageHub.Instance,
                    new AmbientInput(), new AmbientSceneManager());
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            _gameController.Update();
        }
    }
}
