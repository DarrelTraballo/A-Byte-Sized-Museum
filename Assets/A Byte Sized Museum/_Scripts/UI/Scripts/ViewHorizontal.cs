using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class ViewHorizontal : CustomUIComponent
    {
        public ViewSO viewData;

        public GameObject containerLeft;
        public GameObject containerRight;

        private Image imageLeft;
        private Image imageRight;

        private HorizontalLayoutGroup horizontalLayoutGroup;

        public override void Setup()
        {
            horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();

            imageLeft = containerLeft.GetComponent<Image>();
            imageRight = containerRight.GetComponent<Image>();
        }

        public override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) return;

            horizontalLayoutGroup.padding = viewData.padding;
            horizontalLayoutGroup.spacing = viewData.spacing;

            imageLeft.color = theme.primaryBG;
            imageRight.color = theme.secondaryBG;
        }
    }
}
