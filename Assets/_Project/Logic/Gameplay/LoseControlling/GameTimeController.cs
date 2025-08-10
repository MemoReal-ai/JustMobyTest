using System;
using _Project.Logic.Gameplay.PlayerLogic;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Gameplay.LoseControlling
{
    public class GameTimeController : IInitializable
    {

        public void Initialize()
        {
            StartGame();
        }
        
        private void StartGame()
        {
            Time.timeScale = 1;
        }

        public void LoseGame()
        {
            Time.timeScale = 0;
            Debug.Log("Game Over");
        }
    }
}