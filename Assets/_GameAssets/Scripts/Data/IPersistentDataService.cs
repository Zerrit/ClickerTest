using System;
using Cysharp.Threading.Tasks;

namespace ClickerTest.Data
{
    public interface IPersistentDataService
    {
        public event Action OnNeedSaving;

        public PlayerProgress Progress { get;}
    }
}