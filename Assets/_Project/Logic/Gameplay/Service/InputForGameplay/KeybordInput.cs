using System;
using _Project.Logic.Gameplay.Service.InputForGameplay;
using UnityEngine;
using Zenject;

public class KeybordInput : IInput, ITickable
{
    public Action OnShoot { get; set; }
    
    private KeyCode _buttonForShoot = KeyCode.KeypadEnter;

    public void Tick()
    {
        if (Input.GetKeyDown(_buttonForShoot))
        {
            OnShoot?.Invoke();
        }
    }

    public float GetAxisHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetAxisVertical()
    {
        return Input.GetAxis("Vertical");
    }
}