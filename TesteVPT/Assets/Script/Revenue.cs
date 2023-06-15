using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Revenue : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;

    public void SetInfo(Sprite s, string t){
        image.sprite = s;
        text.text = t;
    }
}
