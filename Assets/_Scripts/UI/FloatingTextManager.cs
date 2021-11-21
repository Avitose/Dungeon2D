using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    private readonly List<FloatingText> floatingTexts = new List<FloatingText>();
    public GameObject textContainer;
    public GameObject textPrefab;


    private void Update()
    {
        foreach (var txt in floatingTexts) txt.UpdateFloatingText();
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        var FloatingText = GetFloatingText();

        FloatingText.text.text = msg;
        FloatingText.text.fontSize = fontSize;
        FloatingText.text.color = color;


        FloatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);

        FloatingText.motion = motion;

        FloatingText.duration = duration;
        FloatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        var txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.text = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}