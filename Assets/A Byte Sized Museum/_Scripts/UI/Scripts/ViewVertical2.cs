using UnityEngine;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class ViewVertical2 : CustomUIComponent
    {
        public ViewSO viewData;

        public GameObject containerTop;
        public GameObject containerBottom;

        private Image imageTop;
        private Image imageBottom;

        private VerticalLayoutGroup verticalLayoutGroup;

        public override void Setup()
        {
            verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            
            imageTop = containerTop.GetComponent<Image>();
            imageBottom = containerBottom.GetComponent<Image>();
        }

        public override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) return;

            verticalLayoutGroup.padding = viewData.padding;
            verticalLayoutGroup.spacing = viewData.spacing;

            imageTop.color = theme.secondaryBG;
            imageBottom.color = theme.secondaryBG;
        }
    }
}
