using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Image characterSprite;

    private int currentCharacterSelection;
    public Text hitpointText, pesosText, upgradeCostText, xpText;

    public Text levelTextMenu;

    public GameObject savingTextObject;
    public Image weaponSprite;


    public RectTransform xpBar;

    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }


    private void OnSelectionChanged()
    {
        characterSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }


    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }


    public void UpdateMenu()
    {
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];


        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text =
                GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        levelTextMenu.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitPoint + " /" + GameManager.instance.player.maxHitPoint;
        pesosText.text = GameManager.instance.pesos.ToString();


        var currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience + " total exprience points";
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
            xpText.text = currXpIntoLevel + " / " + diff;
        }
    }

    public void ShowSavingText()
    {
        if (savingTextObject.activeSelf != true)
        {
            savingTextObject.SetActive(true);
            Invoke("HideSavingText", 2);
        }
        else
        {
        }
    }

    private void HideSavingText()
    {
        savingTextObject.SetActive(false);
    }
}