using UnityEngine;

namespace Gameplay.Deaths
{
    public class GameOverPanelEnabler : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;

        private void OnEnable()
        {
            DeathsCounter.OnGameFinishes.AddListener(EnableFinishGamePanel);
        }

        private void OnDisable()
        {
            DeathsCounter.OnGameFinishes.RemoveListener(EnableFinishGamePanel);
        }

        private void EnableFinishGamePanel()
        {
            gameOverPanel.SetActive(true);
        }
    }
}