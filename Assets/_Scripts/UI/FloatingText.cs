using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public float duration;
    public GameObject go;
    public float lastshown;
    public Vector3 motion;
    public Text text;


    public void Show()
    {
        active = true;
        lastshown = Time.time;
        go.SetActive(active);
    }


    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }


    public void UpdateFloatingText()
    {
        if (!active)
            return;


        if (Time.time - lastshown > duration)
            Hide();

        go.GetComponent<Transform>().position += motion * Time.deltaTime;
    }
}