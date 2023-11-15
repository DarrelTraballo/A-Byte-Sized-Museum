using TMPro;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Text : CustomUIComponent
    {
        public TextSO textData;
        public Style style;

        private TextMeshProUGUI textMeshProUGUI;

        public override void Setup()
        {
            textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        public override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) return;

            textMeshProUGUI.color = theme.GetTextColor(style);
            textMeshProUGUI.font = textData.font;
            textMeshProUGUI.fontSize = textData.size;
        }

        public void SetText(string text)
        {
            textMeshProUGUI.text = text;
        }
    }
}
