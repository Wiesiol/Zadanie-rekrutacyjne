using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.SelectAmountPanel
{
    public class ObjectsAmountButton : MonoBehaviour
    {
        [SerializeField] private int objectsAmount = 50;
        [SerializeField] private Button button;
        [SerializeField] private Sprite selectedSprite;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private RectTransform verticalLayoutGroup;
        [SerializeField] private TextMeshProUGUI ammountDisplay;

        private static readonly UnityEvent<GameObject> OnSelectionChange = new();

        private SelectionChanger selectionChanger;

        private void Awake()
        {
            selectionChanger = new(gameObject, normalSprite, selectedSprite, button, verticalLayoutGroup);
            ammountDisplay.SetText(objectsAmount.ToString());
        }

        private void OnEnable()
        {
            button.onClick.AddListener(ChangeAmount);
            OnSelectionChange.AddListener(selectionChanger.ChangeSelection);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(ChangeAmount);
            OnSelectionChange.RemoveListener(selectionChanger.ChangeSelection);
        }

        private void ChangeAmount()
        {
            StartButton.OnAmountSelected.Invoke(objectsAmount);

            OnSelectionChange.Invoke(gameObject);
        }
    }
}