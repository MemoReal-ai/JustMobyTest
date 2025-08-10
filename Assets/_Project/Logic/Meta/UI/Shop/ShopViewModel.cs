using System;
using _Project.Logic.Gameplay.PlayerLogic;
using _Project.Logic.Meta.Shop;
using R3;
using Zenject;

namespace _Project.Logic.Meta.UI.Shop
{
    public class ShopViewModel : IInitializable, IDisposable
    {
        private readonly WalletPlayer _wallet;
        private readonly ShopController _shopController;
        private readonly ShopView _view;

        private readonly ReactiveProperty<int> _points = new();
        private readonly ReactiveProperty<int> _healthCost = new();
        private readonly ReactiveProperty<int> _speedCost = new();
        private readonly ReactiveProperty<int> _damageCost = new();
        private readonly ReactiveProperty<int> _healthLevel = new();
        private readonly ReactiveProperty<int> _speedLevel = new();
        private readonly ReactiveProperty<int> _damageLevel = new();

        private readonly ReactiveCommand _upgradeHealthCommand = new();
        private readonly ReactiveCommand _upgradeSpeedCommand = new();
        private readonly ReactiveCommand _upgradeDamageCommand = new();
        private readonly ReactiveCommand _exitCommand = new();
        private readonly ReactiveCommand _showShopCommand = new();
        private readonly ReactiveCommand _applyUpgradesCommand = new();

        public ShopViewModel(WalletPlayer wallet, ShopController shopManager, ShopView view)
        {
            _wallet = wallet;
            _shopController = shopManager;
            _view = view;
        }

        public void Initialize()
        {
            _wallet.OnPointsChanged += UpdatePoints;
            SetupCommands();
            SetupBindings();
            UpdateAllValues();
        }

        public void Dispose()
        {
            _wallet.OnPointsChanged -= UpdatePoints;

            _upgradeHealthCommand.Dispose();
            _upgradeSpeedCommand.Dispose();
            _upgradeDamageCommand.Dispose();
            _exitCommand.Dispose();
            _showShopCommand.Dispose();
            _applyUpgradesCommand.Dispose();

            _points.Dispose();
            _healthCost.Dispose();
            _speedCost.Dispose();
            _damageCost.Dispose();
            _healthLevel.Dispose();
            _speedLevel.Dispose();
            _damageLevel.Dispose();
        }

        private void UpdatePoints(int points)
        {
            _points.Value = points;
        }

        private void SetupCommands()
        {
            _upgradeHealthCommand.Subscribe(_ => UpgradeHealth());
            _upgradeSpeedCommand.Subscribe(_ => UpgradeSpeed());
            _upgradeDamageCommand.Subscribe(_ => UpgradeDamage());
            _exitCommand.Subscribe(_ => _view.HideShop());
            _showShopCommand.Subscribe(_ => _view.ShowShop());
            _applyUpgradesCommand.Subscribe(_ => _view.HideShop());
        }

        private void SetupBindings()
        {
            _view.UpgradeHealthButton.OnClickAsObservable()
                .Subscribe(_upgradeHealthCommand.Execute);

            _view.UpgradeSpeedButton.OnClickAsObservable()
                .Subscribe(_upgradeSpeedCommand.Execute);

            _view.UpgradeDamageButton.OnClickAsObservable()
                .Subscribe(_upgradeDamageCommand.Execute);
          
            _view.ExitButton.OnClickAsObservable()
                .Subscribe(_exitCommand.Execute);

            _view.ShowShopButton.OnClickAsObservable()
                .Subscribe(_showShopCommand.Execute);

            _view.ApplyUpgradeButton.OnClickAsObservable()
                .Subscribe(_applyUpgradesCommand.Execute);

            _points.Subscribe(p => _view.SetPoints(p));
            _healthLevel.Subscribe(l => _view.SetLevelHealth(l));
            _speedLevel.Subscribe(l => _view.SetLevelSpeed(l));
            _damageLevel.Subscribe(l => _view.SetLevelDamage(l));
            _healthCost.Subscribe(c => _view.SetCostHealth(c));
            _speedCost.Subscribe(c => _view.SetCostSpeed(c));
            _damageCost.Subscribe(c => _view.SetCostDamage(c));
        }

        private void UpdateAllValues()
        {
            _points.Value = _wallet.GetPoints();
            _healthLevel.Value = _shopController.CurrentHealthLevel;
            _speedLevel.Value = _shopController.CurrentSpeedLevel;
            _damageLevel.Value = _shopController.CurrentDamageLevel;
            _healthCost.Value = _shopController.HealthUpgradeCost;
            _speedCost.Value = _shopController.SpeedUpgradeCost;
            _damageCost.Value = _shopController.DamageUpgradeCost;
        }

        private void UpgradeHealth()
        {
            if (!_wallet.TrySpend(_shopController.HealthUpgradeCost)) return;

            _shopController.TryUpgradeHealth();
            _healthLevel.Value = _shopController.CurrentHealthLevel;
            _healthCost.Value = _shopController.HealthUpgradeCost;
        }

        private void UpgradeSpeed()
        {
            if (!_wallet.TrySpend(_shopController.SpeedUpgradeCost)) return;

            _shopController.TryUpgradeSpeed();
            _speedLevel.Value = _shopController.CurrentSpeedLevel;
            _speedCost.Value = _shopController.SpeedUpgradeCost;
        }

        private void UpgradeDamage()
        {
            if (!_wallet.TrySpend(_shopController.DamageUpgradeCost)) return;

            _shopController.TryUpgradeDamage();
            _damageLevel.Value = _shopController.CurrentDamageLevel;
            _damageCost.Value = _shopController.DamageUpgradeCost;
        }
    }
}