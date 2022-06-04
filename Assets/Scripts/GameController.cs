using UnityEngine;

public class GameController : MonoBehaviour
{
    // [Optimization]
    // Since I am using one game controller with multiple canvases across all of the game, these properties are set manually
    // Reason behind this is to save CPU from running GetComponentInChildren through all of children entities several times just to find every canvas controllers
    public MainCanvasController mainCanvasController;
    public DailyRewardCanvasController dailyRewardCanvasController;

    private GameObject mainCanvas;
    private GameObject dailyRewardCanvas;

    private void Awake()
    {
        mainCanvas = mainCanvasController.gameObject;
        dailyRewardCanvas = dailyRewardCanvasController.gameObject;
    }

    // Enabling main canvas at the game launch in case it was turned off
    // However, I might be turning it off deliberately to edit other canvases
    private void Start()
    {
        mainCanvas.SetActive(true);
    }

    // Toggling daily reward menu on/off
    public void ToggleDailyRewardCanvas(bool desiredRewardCanvasState)
    {
        mainCanvas.SetActive(!desiredRewardCanvasState);
        dailyRewardCanvas.SetActive(desiredRewardCanvasState);
    }
}
