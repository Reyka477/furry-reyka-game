using UnityEngine;

public class TabManager : MonoBehaviour
{
    [Header("Все вкладки, которые ты будешь включать/выключать")]
    public GameObject[] allTabs;

    public void ShowTab(GameObject selectedTab)
    {
        foreach (GameObject tab in allTabs)
        {
            tab.SetActive(tab == selectedTab);
        }
    }
}