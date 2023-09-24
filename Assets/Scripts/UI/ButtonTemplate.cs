using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ButtonTemplate : MonoBehaviour
    {
        private Button button;

        protected void Awake()
        {
            button = GetComponent<Button>();
        }

        protected void OnEnable()
        {
            button.onClick.AddListener(OnClickMethod);
        }

        protected void OnDisable()
        {
            button.onClick.RemoveListener(OnClickMethod);
        }

        protected abstract void OnClickMethod();
    }
}
