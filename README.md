# Daily Reward System

## Basic 7-day daily reward menu example

System that allows player to get rewards if they are opening app (and collecting rewards) every day.
Currently it has no connection to actual rewards, it just has functionality of day-to-day checklist.
If user skips a day, app revert all progress to day 1, if user has collected all rewards from last 7 days, app reverts to day 1.
Works with different months and years, not only days 
(e.g. if user has collected reward at 31 December 2021, he will get next day reward at 1 January 2022 as intended)

## Installing 
1. Install Unity Editor (ver. 2020.3.34f1 or newer) via Unity Hub
2. Clone repository
3. Open Daily-Reward project via "Open" button in Unity Hub
4. Press "Play" button OR press Ctrl+B to Build & Run application

## Testing
Application contains [PlayerPrefsEditor](https://assetstore.unity.com/packages/tools/utilities/playerprefs-editor-167903) 
Asset, which you can load using Tools -> BG Tools -> PlayerPrefs Editor.

Change DailyReward(-Day, -Month, -Year) to change last time user collected reward and DailyRewardSequence to change how many times user has collected reward.
