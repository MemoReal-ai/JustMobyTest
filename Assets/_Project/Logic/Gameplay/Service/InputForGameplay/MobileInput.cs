using System;

namespace _Project.Logic.Gameplay.Service.InputForGameplay
{
    public class MobileInput : IInput
    {
        public Action OnShoot { get; set; }
        
        private readonly Joystick _joystick;
        public MobileInput(Joystick joystick)
        {
            _joystick = joystick;
        }
        
        public float GetAxisHorizontal()
        {
            var horizontal = _joystick.Horizontal;
            return horizontal;
        }

        public float GetAxisVertical()
        {
            var vertical = _joystick.Vertical;
            return vertical;
        }
    }
}
