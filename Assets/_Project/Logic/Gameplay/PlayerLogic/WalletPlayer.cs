using System;
using UnityEngine;

namespace _Project.Logic.Gameplay.PlayerLogic
{
    public class WalletPlayer
    {
        public event Action<int> OnPointsChanged;

        private int Points { get; set; }

        public void AddPoints(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount must be positive");

            Points += amount;
            OnPointsChanged?.Invoke(Points);
        }

        private bool CanSpend(int amount) => Points >= amount;

        public bool TrySpend(int amount)
        {
            if (CanSpend(amount)==false)
            {
                Debug.LogWarning($"Not enough points! Current: {Points}, Required: {amount}");
                return false;
            }

            Points -= amount;
            OnPointsChanged?.Invoke(Points);
            return true;
        }

        public int GetPoints()
        {
            return Points;
        }
    }
}