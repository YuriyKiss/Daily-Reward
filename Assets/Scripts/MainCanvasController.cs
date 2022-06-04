using UnityEngine;

public class MainCanvasController : MonoBehaviour
{
    private GameController gameController;

    private void Awake()
    {
        gameController = GetComponentInParent<GameController>();
    }

    public void OnDailyRewardButtonPressed()
    {
        gameController.ToggleDailyRewardCanvas(true);
    }
}
