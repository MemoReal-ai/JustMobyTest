using System;
using _Project.Logic.Gameplay.Service.InputForGameplay;
using UnityEngine;
using Zenject;

namespace _Project.Logic.Gameplay.PlayerLogic
{
    public class PlayerController : ITickable, IInitializable, IDisposable
    {
        private readonly Player _player;
        private readonly IInput _input;

        private Vector3 _directionToMove;

        public PlayerController(Player player, IInput input)
        {
            _player = player;
            _input = input;
        }

        public void Initialize()
        {
            _input.OnShoot = _player.Shoot;
        }

        public void Tick()
        {
            HandleInput();
            _player.Move(_directionToMove);
        }

        public void Dispose()
        {
            _input.OnShoot -= _player.Shoot;
        }

        private void HandleInput()
        {
            var horizontalAxis = _input.GetAxisHorizontal();
            var verticalAxis = _input.GetAxisVertical();
            _directionToMove = new Vector3(horizontalAxis, 0, verticalAxis).normalized;
        }
    }
}