using UnityEngine;

public class TabButton : MonoBehaviour
{
    public TabManager tabManager;
    public GameObject targetTab;

    public void OnClick()
    {
        tabManager.ShowTab(targetTab);
    }
}