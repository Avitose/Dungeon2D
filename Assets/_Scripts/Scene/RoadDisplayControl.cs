using UnityEngine;

public class RoadDisplayControl : MonoBehaviour
{
    public GameObject enemys;
    private int num;
    public GameObject trans;

    private void Start()
    {
        trans.gameObject.SetActive(false);
    }

    private void Update()
    {
        num = enemys.transform.childCount;

        if (num == 0) DisplayRoad();
    }

    public void DisplayRoad()
    {
        trans.gameObject.SetActive(true);
    }
}