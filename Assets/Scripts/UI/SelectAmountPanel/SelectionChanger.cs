using UnityEngine;
using UnityEngine.UI;

namespace UI.SelectAmountPanel
{
    public class SelectionChanger
    {
        private readonly GameObject gameObject;
        private readonly Sprite normalSprite;
        private readonly Sprite selectedSprite;
        private Button button;
        private RectTransform verticalLayoutGroup;

        public SelectionChanger(GameObject gameObject, Sprite normalSprite, Sprite selectedSprite, 
            Button button, RectTransform verticalLayoutGroup)
        {
            this.gameObject = gameObject;
            this.normalSprite = normalSprite;
            this.selectedSprite = selectedSprite;
            this.button = button;
            this.verticalLayoutGroup = verticalLayoutGroup;
        }

        public void ChangeSelection(GameObject selectedGameObject)
        {
            if (selectedGameObject != gameObject)
            {
                ChangeButtonSpriteAndLoalScale(normalSprite, Vector3.one);

                return;
            }

            ChangeButtonSpriteAndLoalScale(selectedSprite, Vector3.one * 1.1f);
        }

        private void ChangeButtonSpriteAndLoalScale(Sprite sprite, Vector3 scale)
        {
            var buttonImage = button.image;
            var buttonTransform = buttonImage.transform;

            buttonImage.sprite = sprite;
            buttonTransform.localScale = scale;

            LayoutRebuilder.ForceRebuildLayoutImmediate(verticalLayoutGroup);
        }
    }
}