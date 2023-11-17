using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class CustomButton : CustomUIComponent
    {
        // public ThemeSO theme;
        public Style style;
        public UnityEvent onClick;

        private Button button;
        private TextMeshProUGUI buttonText;

        public override void Setup()
        {
            button = GetComponentInChildren<Button>();
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) return;

            ColorBlock cb = button.colors;
            cb.normalColor = theme.GetBackgroundColor(style);
            button.colors = cb;

            buttonText.color = theme.GetTextColor(style);
        }

        public void OnClick()
        {
            onClick.Invoke();
        }
    }
}
