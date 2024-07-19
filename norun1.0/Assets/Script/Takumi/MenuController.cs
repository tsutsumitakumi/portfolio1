using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuObject;

    void Start()
    {
        // 最初はメニューを非表示にする
        menuObject.SetActive(false);
    }

    public void ToggleMenu()
    {
        // メニューの表示・非表示を切り替える
        menuObject.SetActive(!menuObject.activeSelf);
    }
}
