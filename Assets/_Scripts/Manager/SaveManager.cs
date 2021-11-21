using System.IO;
using LitJson;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SetGameData(Save save)
    {
        GameManager.instance.pesos = save.pesos;
        GameManager.instance.experience = save.experience;
        GameManager.instance.weapon.SetWeaponLevel(save.WeaponLevel);
        GameManager.instance.player.rage = save.rage;
    }

    public void SaveGame()
    {
        var save = new Save
        {
            pesos = GameManager.instance.pesos,
            experience = GameManager.instance.experience,
            WeaponLevel = GameManager.instance.weapon.weaponLevel,
            rage = (int) GameManager.instance.player.rage
        };

        var path = Application.dataPath + "/SaveData.json";


        var jsonStr = JsonMapper.ToJson(save);


        var sw = new StreamWriter(path);
        sw.Write(jsonStr);
        sw.Close();

        Debug.Log("Saves");
    }


    public void SaveGame(Save save)
    {
        var path = Application.dataPath + "/SaveData.json";
        var jsonStr = JsonMapper.ToJson(save);

        var sw = new StreamWriter(path);
        sw.Write(jsonStr);
        sw.Close();

        Debug.Log("Saves");
    }


    public void LoadGame()
    {
        var path = Application.dataPath + "/SaveData.json";

        if (File.Exists(path))
        {
            var sr = new StreamReader(path);


            var jsonStr = sr.ReadToEnd();
            sr.Close();

            var save = JsonMapper.ToObject<Save>(jsonStr);
            SetGameData(save);

            Debug.Log("Game Loaded");
        }
        else
        {
            NewGame();
            LoadGame();
        }
    }

    public void NewGame()
    {
        var save = new Save
        {
            pesos = 0,
            experience = 0,
            WeaponLevel = 0,
            rage = 0
        };
        SaveGame(save);
    }
}