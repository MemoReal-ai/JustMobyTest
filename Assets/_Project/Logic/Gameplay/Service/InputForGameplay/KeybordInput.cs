using _Project.Logic.Gameplay.Service.InputForGameplay;
using UnityEngine;

public class KeybordInput : IInput
{
    public float GetAxisHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetAxisVertical()
    {
        return Input.GetAxis("Vertical");
    }
}