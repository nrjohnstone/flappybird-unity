using System;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class BaseColumnSpawner
    {
        protected Func<IGameObject> ColumnFactory;
        protected IGameObject[] Columns;
        public int ColumnPoolSize { get; set; }

        public void Initialize()
        {
            Columns = new IGameObject[ColumnPoolSize];
            for (int i = 0; i < ColumnPoolSize; i++)
            {
                Columns[i] = ColumnFactory.Invoke();
            }
        }

        public Vector2 GetColumnPosition(int i)
        {
            return Columns[i].transform.position;
        }
    }
}