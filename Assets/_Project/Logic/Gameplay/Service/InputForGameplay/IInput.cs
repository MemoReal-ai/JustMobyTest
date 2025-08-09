using System;

namespace _Project.Logic.Gameplay.Service.InputForGameplay
{
    public interface IInput
    {
        Action OnShoot { get; set; }
        float GetAxisHorizontal();
        float GetAxisVertical();
    }
}