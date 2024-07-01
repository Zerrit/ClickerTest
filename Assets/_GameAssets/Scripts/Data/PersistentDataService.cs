using System;
using System.Threading;
using ClickerTest.Configs;
using ClickerTest.Tools.DataManipulation;
using UnityEngine;
using VContainer.Unity;

namespace ClickerTest.Data
{
    public class PersistentDataService : IPersistentDataService, IStartable
    {
        public event Action OnNeedSaving;

        public PlayerProgress Progress { get; private set; }
        private CancellationTokenSource _cts;

        private readonly StartProgressConfig _startConfig;
        private readonly JsonSaveLoader _saveLoader;

        public PersistentDataService(StartProgressConfig startConfig)
        {
            _startConfig = startConfig;
            _saveLoader = new JsonSaveLoader();
        }

        public void Start()
        {
            if (_saveLoader.HasExistData())
            {
                Progress = _saveLoader.TryLoadData();
            }
            else
            {
                Progress = new PlayerProgress(_startConfig.Points, _startConfig.Level, _startConfig.ClickCount, 
                    _startConfig.PointsPerClick, _startConfig.LevelUpRequirement, _startConfig.LevelUpRequirement);

                Save();
            }

            Application.quitting += Save;

            Debug.Log("Сохранения инициализированы");
        }

        public void Save()
        {
            OnNeedSaving?.Invoke();

            _saveLoader.SaveData(Progress);
        }
    }
}