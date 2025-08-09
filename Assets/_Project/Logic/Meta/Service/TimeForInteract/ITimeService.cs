using UnityEngine;

namespace _Project.Logic.Gameplay.Service.TimeForInteract
{
    public interface ITimeService
    {
        float GetDeltaTime();
        float GetFixedDeltaTime();
    }
}
