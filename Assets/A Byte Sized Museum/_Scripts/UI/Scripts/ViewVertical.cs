using UnityEngine;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class ViewVertical : CustomUIComponent
    {
        public ViewSO viewData;

        public GameObject containerTop;
        public GameObject containerCenter;
        public GameObject containerBottom;

        private Image imageTop;
        private Image imageCenter;
        private Image imageBottom;

        private VerticalLayoutGroup verticalLayoutGroup;

        public override void Setup()
        {
            verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            
            imageTop = containerTop.GetComponent<Image>();
            imageCenter = containerCenter.GetComponent<Image>();
            imageBottom = containerBottom.GetComponent<Image>();
        }

        public override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) return;

            verticalLayoutGroup.padding = viewData.padding;
            verticalLayoutGroup.spacing = viewData.spacing;

            imageTop.color = theme.tertiaryBG;
            imageCenter.color = theme.secondaryBG;
            imageBottom.color = theme.tertiaryBG;
        }
    }
}
