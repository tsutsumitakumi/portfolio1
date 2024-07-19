using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tojiruButton : MonoBehaviour
{

    public GameObject popup;
    // Start is called before the first frame update
    public void OnClick()
    {
        popup.SetActive(false);
    }
}
