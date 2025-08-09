using UnityEngine;

namespace _Project.Logic.Gameplay.Service.InputForGameplay
{
    public class MobileInput : IInput
    {
        public float GetAxisHorizontal()
        {
            return Input.GetAxis("Horizontal");
        }

        public float GetAxisVertical()
        {
            throw new System.NotImplementedException();
        }
    }
}
