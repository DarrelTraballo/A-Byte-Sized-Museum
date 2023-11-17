using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [DefaultExecutionOrder(-1)]
    public class ThemeManager : MonoBehaviour
    {
        [SerializeField]
        private ThemeSO mainTheme;

        public static ThemeManager Instance;
        private ThemeManager() {}

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else 
                Instance = this;
        }

        public ThemeSO GetMainTheme()
        {
            return mainTheme;
        }
    }
}
