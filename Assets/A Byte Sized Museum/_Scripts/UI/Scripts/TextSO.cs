using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [CreateAssetMenu(menuName = "CustomUI/TextSO", fileName = "Text")]
    public class TextSO : ScriptableObject
    {
        public ThemeSO theme;

        public TMP_FontAsset font;
        public float size;
    }
}
