using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CharacterHUD characterHUD;
    public CharacterMenu characterMenu;

    public Animator deathMenuAnim;
    public FloatingTextManager floatingTextManager;


    private void Start()
    {
        deathMenuAnim.gameObject.SetActive(false);
        UIUpdate();
    }

    public void UIUpdate()
    {
        characterMenu.UpdateMenu();
        characterHUD.UpdateHUD();
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public void HideDeathAnimation()
    {
        deathMenuAnim.SetTrigger("Hide");
        deathMenuAnim.gameObject.SetActive(false);
    }

    public void ShowDeathAnimation()
    {
        deathMenuAnim.gameObject.SetActive(true);
        deathMenuAnim.SetTrigger("Show");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}