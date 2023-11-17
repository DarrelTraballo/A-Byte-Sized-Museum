using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public abstract class CustomUIComponent : MonoBehaviour
    {
        public ThemeSO overwriteTheme;

        private void Awake()
        {
            Init();
        }

        public abstract void Setup();
        public abstract void Configure();

        private void Init()
        {
            Setup();
            Configure();
        }

        private void OnValidate()
        {
            Init();
        }

        protected ThemeSO GetMainTheme()
        {
            if (overwriteTheme != null)
                return overwriteTheme;
            else if (ThemeManager.Instance != null)
                return ThemeManager.Instance.GetMainTheme();

            return null;
        }
    }
}
