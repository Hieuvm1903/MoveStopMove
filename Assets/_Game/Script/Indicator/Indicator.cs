using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    public TextMeshProUGUI levelText;
    private void Start()
    {

    }
    public void SetImageColor(Color color)
    {
        image.color = color;
    }
    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }
    public void SetTextRotationAndPosition(Quaternion rotation,Vector3 position)
    {
        levelText.rectTransform.rotation = rotation;
        levelText.rectTransform.position = position;
    }
}
