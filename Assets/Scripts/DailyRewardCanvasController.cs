using System;
using UnityEngine;
using UnityEngine.UI;

// All in all, this script should be further upgraded to fetch reward type, and actually reward player
public class DailyRewardCanvasController : MonoBehaviour
{
    public RectTransform dailyRewards;
    public Button claimDailyRewardButton;

    private GameController gameController;

    private DateTime today;
    private DailyRewardFieldController dailyRewardFieldController;

    private int dailyRewardDay;
    private int dailyRewardMonth;
    private int dailyRewardYear;
    private int dailyRewardDaySequence;

    private void Awake()
    {
        gameController = GetComponentInParent<GameController>();
    }

    private void Start()
    {
        today = DateTime.Now;

        CheckPlayerPreferencesExistance();

        FetchPlayerPreferences();

        if (UserActivatedDailyRewardToday())
        {
            DeactivateCurrentDailyReward();
        }
        else
        {
            CheckDailyRewardContinuity();

            HighlightCurrentDailyReward();
        }

        DeactivateClaimedDailyRewards();
    }

    // PlayerPrefs description:
    // DailyRewardDay - last day that daily reward was activated
    // DailyRewardMonth - last month that daily reward was activated
    // DailyRewardYear - last year that daily reward was activated
    // DailyRewardDaySequence - total days daily reward was activated in row
    private void CheckPlayerPreferencesExistance()
    {
        if (!PlayerPrefs.HasKey("DailyRewardDay") || !PlayerPrefs.HasKey("DailyRewardMonth") ||
            !PlayerPrefs.HasKey("DailyRewardYear") || !PlayerPrefs.HasKey("DailyRewardDaySequence"))
        {
            PlayerPrefs.SetInt("DailyRewardDay", 0);
            PlayerPrefs.SetInt("DailyRewardMonth", 0);
            PlayerPrefs.SetInt("DailyRewardYear", 0);
            PlayerPrefs.SetInt("DailyRewardDaySequence", 0);
        }
    }

    private void FetchPlayerPreferences()
    {
        dailyRewardDay = PlayerPrefs.GetInt("DailyRewardDay");
        dailyRewardMonth = PlayerPrefs.GetInt("DailyRewardMonth");
        dailyRewardYear = PlayerPrefs.GetInt("DailyRewardYear");
        dailyRewardDaySequence = PlayerPrefs.GetInt("DailyRewardDaySequence");
    }

    private bool UserActivatedDailyRewardToday()
    {
        return dailyRewardDay == today.Day && dailyRewardMonth == today.Month && dailyRewardYear == today.Year;
    }

    // If user claimed daily reward already we should deactivate today's reward field
    private void DeactivateCurrentDailyReward()
    {
        Transform activatedDailyRewardFieldTransform = dailyRewards.GetChild(dailyRewardDaySequence - 1);
        DailyRewardFieldController activatedDailyRewardFieldController = activatedDailyRewardFieldTransform.GetComponent<DailyRewardFieldController>();
        activatedDailyRewardFieldController.RewardDeactivate();

        claimDailyRewardButton.interactable = false;
    }

    // Checks whether user missed any days
    private void CheckDailyRewardContinuity()
    {
        if ((dailyRewardDay + 1 == today.Day && dailyRewardMonth == today.Month     && dailyRewardYear == today.Year    ) || 
            (dailyRewardDay == 1             && dailyRewardMonth + 1 == today.Month && dailyRewardYear == today.Year    ) ||
            (dailyRewardDay == 1             && dailyRewardMonth == 1               && dailyRewardYear + 1 == today.Year))
        {
            return;
        }

        dailyRewardDaySequence = 0;
    }

    // If user hasn't claimed reward yet, we highlight which reward they have will claim today
    private void HighlightCurrentDailyReward()
    {
        Transform currentDailyRewardFieldTransform = dailyRewards.GetChild(dailyRewardDaySequence);
        dailyRewardFieldController = currentDailyRewardFieldTransform.GetComponent<DailyRewardFieldController>();

        dailyRewardFieldController.Highlight();
    }

    public void OnClaimRewardButtonPressed()
    {
        dailyRewardFieldController.RewardClaimedAction();

        PlayerPrefs.SetInt("DailyRewardDay", today.Day);
        PlayerPrefs.SetInt("DailyRewardMonth", today.Month);
        PlayerPrefs.SetInt("DailyRewardYear", today.Year);

        dailyRewardDaySequence++;
        if (dailyRewardDaySequence < dailyRewards.childCount)
        {
            PlayerPrefs.SetInt("DailyRewardDaySequence", dailyRewardDaySequence);
        }
        else
        {
            PlayerPrefs.SetInt("DailyRewardDaySequence", 0);
        }

        claimDailyRewardButton.interactable = false;
    }

    // All claimed rewards should be deactivated
    private void DeactivateClaimedDailyRewards()
    {
        for (int i = 0; i < dailyRewardDaySequence; ++i)
        {
            Transform iteratedDailyRewardFieldTransform = dailyRewards.GetChild(i);
            DailyRewardFieldController iteratedDailyRewardFieldController = iteratedDailyRewardFieldTransform.GetComponent<DailyRewardFieldController>();

            iteratedDailyRewardFieldController.RewardDeactivate();
        }
    }

    public void OnExitDailyRewardCanvasPressed()
    {
        gameController.ToggleDailyRewardCanvas(false);
    }
}
