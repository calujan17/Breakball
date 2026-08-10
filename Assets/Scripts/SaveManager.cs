using UnityEngine;

public class SaveManager : MonoBehaviour {
    private const string CurrentLevelKey = "CurrentLevel";

    public int CurrentLevel { get; private set; }

    private void Awake() {
        LoadData();
    }

    public void LoadData() {
        CurrentLevel = PlayerPrefs.GetInt(CurrentLevelKey, 1);
    }

    public void SaveData() {
        PlayerPrefs.SetInt(CurrentLevelKey, CurrentLevel);
        PlayerPrefs.Save();
    }

    public void SetCurrentLevel(int level) {
        CurrentLevel = level;
        SaveData();
    }

    public void ResetProgress() {
        CurrentLevel = 1;
        SaveData();
    }
}