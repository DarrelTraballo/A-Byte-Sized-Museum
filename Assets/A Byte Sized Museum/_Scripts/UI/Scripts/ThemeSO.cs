using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [CreateAssetMenu(menuName = "CustomUI/ThemeSO", fileName = "Theme")]
    public class ThemeSO : ScriptableObject
    {
        [Header("Primary")]
        public Color primaryBG;
        public Color primaryText;
        
        [Header("Secondary")]
        public Color secondaryBG;
        public Color secondaryText;
        
        
        [Header("Tertiary")]
        public Color tertiaryBG;
        public Color tertiaryText;
        
        
        [Header("Other")]
        public Color disable;

        public Color GetBackgroundColor(Style style)
        {
            if (style == Style.Primary)
                return primaryBG;
            else if (style == Style.Secondary)
                return secondaryBG;
            else if (style == Style.Tertiary)
                return tertiaryBG;

            return disable;
        }

        public Color GetTextColor(Style style)
        {
            if (style == Style.Primary)
                return primaryText;
            else if (style == Style.Secondary)
                return secondaryText;
            else if (style == Style.Tertiary)
                return tertiaryText;

            return disable;
        }
    }
    
    public enum Style
    {
        Primary,
        Secondary,
        Tertiary
    }
}
