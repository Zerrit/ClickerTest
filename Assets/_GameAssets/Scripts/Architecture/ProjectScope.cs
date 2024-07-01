using ClickerTest.Configs;
using ClickerTest.Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClickerTest.Architecture
{
    public class ProjectScope : LifetimeScope
    {
        [SerializeField] private StartProgressConfig _dataConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PersistentDataService>()
                .As<IPersistentDataService>()
                .WithParameter(_dataConfig);
            
            DontDestroyOnLoad(gameObject);
        }
    }
}