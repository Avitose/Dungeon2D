using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : MonoBehaviour
{
    public RectTransform healthBar;

    public Text level;
    public RectTransform rageBar;
    public Image rImage;
    public RectTransform xpBar;


    public void UpdateHUD()
    {
        level.text = GameManager.instance.GetCurrentLevel().ToString();


        var ratio = GameManager.instance.player.hitPoint / (float) GameManager.instance.player.maxHitPoint;
        healthBar.localScale = new Vector3(ratio, 1, 1);


        var currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            xpBar.localScale = Vector3.one;
        }
        else
        {
            var prevLevelXP = GameManager.instance.GetXPToLevel(currentLevel - 1);
            var currLevelXP = GameManager.instance.GetXPToLevel(currentLevel);

            var diff = currLevelXP - prevLevelXP;
            var currXpIntoLevel = GameManager.instance.experience - prevLevelXP;

            var completionRatio = currXpIntoLevel / (float) diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
        }

        rageBar.localScale = new Vector3(GameManager.instance.player.rage / GameManager.instance.player.maxRage, 1, 1);
        if (GameManager.instance.player.rage == GameManager.instance.player.maxRage)
            rImage.gameObject.SetActive(true);
        else
            rImage.gameObject.SetActive(false);
    }
}