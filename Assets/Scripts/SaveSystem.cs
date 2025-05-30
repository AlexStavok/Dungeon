using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveSystem
{
    private static SaveData saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public PlayerStatsData playerStatsData;
        public PotionsData potionsData;
        public SkillsData skillsData;
        public MechanismsData mechanismsData;
        public BarrelsData barrelsData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(saveData, true));
    }
    private static void HandleSaveData()
    {
        PlayerStats.Instance.Save(ref saveData.playerStatsData);
        PotionMenuUI.Instance.Save(ref saveData.potionsData);
        SkillManager.Instance.Save(ref saveData.skillsData);
        MechanicalManager.Instance.Save(ref saveData.mechanismsData);
        BarrelsManager.Instance.Save(ref saveData.barrelsData);
    }
    public static void Load()
    {
        string path = SaveFileName();

        if (!File.Exists(path))
        {
            return;
        }

        string saveContent = File.ReadAllText(path);

        saveData = JsonUtility.FromJson<SaveData>(saveContent);


        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        PlayerStats.Instance.Load(saveData.playerStatsData);
        PotionMenuUI.Instance.Load(saveData.potionsData);
        SkillManager.Instance.Load(saveData.skillsData);
        MechanicalManager.Instance.Load(saveData.mechanismsData);
        BarrelsManager.Instance.Load(saveData.barrelsData);
    }

    public static bool SaveExists()
    {
        return File.Exists(SaveFileName());
    }
}
