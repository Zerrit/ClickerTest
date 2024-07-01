﻿using System.Collections.Generic;
using ClickerTest.Tools.Reactivity;
using ClickerTest.UI;
using UnityEngine;

namespace ClickerTest.MVP.TabsPanel.Model
{
    public class TabsModel
    {
        public SimpleReativeProperty<bool> IsInitialized { get; }

        public int ActiveTabIndex { get; private set; } = -1;
        private readonly Dictionary<int, IScreenModel> _tabModels = new ();

        public TabsModel(IEnumerable<IScreenModel> tabModels)
        {
            IsInitialized = new SimpleReativeProperty<bool>(false);
            Debug.Log(tabModels);
            foreach (var model in tabModels)
            {
                _tabModels[model.ScreenId] = model;
                Debug.Log(model.ScreenId);
            }

            IsInitialized.Value = true;
        }

        public void SwitchTab(int id)
        {
            if(!_tabModels.ContainsKey(id)) return;

            if (id == ActiveTabIndex) return;

            if (_tabModels.ContainsKey(ActiveTabIndex))
            {
                _tabModels[ActiveTabIndex].DisplayingStatus.Value = false;
            }

            ActiveTabIndex = id;
            _tabModels[ActiveTabIndex].DisplayingStatus.Value = true;
            
        }
    }
}