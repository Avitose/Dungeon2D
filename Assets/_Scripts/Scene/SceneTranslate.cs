using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTranslate : MonoBehaviour
{
    private float dtimer;

    private AsyncOperation op;
    public Canvas SCUI;
    private Slider SCUISlider;
    private float target;

    private void Start()
    {
        if (SCUI == null)
            SCUI = GameObject.Find("SCUI").GetComponent<Canvas>();

        SCUI.gameObject.SetActive(false);
        SCUISlider = SCUI.GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        UpdateSlider();
    }

    public void ChangeToScene(string sceneName)
    {
        SCUI.gameObject.SetActive(true);
        SCUISlider.value = 0;

        StartCoroutine(ProcessLoading(sceneName));
    }

    private void UpdateSlider()
    {
        if (op != null)
        {
            if (op.progress >= 0.9f)
                target = 1;
            else
                target = op.progress;

            if (SCUISlider.value > 0.99)
            {
                SCUISlider.value = 1;
                SCUI.gameObject.SetActive(false);
                op.allowSceneActivation = true;
                return;
            }

            SCUISlider.value = Mathf.Lerp(SCUISlider.value, target, dtimer * 0.05f);
            dtimer += Time.deltaTime;
        }
    }

    private IEnumerator ProcessLoading(string sceneName)
    {
        op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        yield return op;
    }
}