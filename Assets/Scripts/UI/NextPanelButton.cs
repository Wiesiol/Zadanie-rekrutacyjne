using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [System.Serializable]
    public class NextPanelButton : ButtonTemplate
    {
        [SerializeField] private GameObject panelToDisable;
        [SerializeField] private GameObject panelToEnable;

        protected override void OnClickMethod()
        {
            SwitchPanelToNext();
        }

        private void SwitchPanelToNext()
        {
            panelToEnable.SetActive(true);
            panelToDisable.SetActive(false);
        }
    }
}
