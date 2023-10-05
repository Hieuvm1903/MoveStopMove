using UnityEngine;
using System.Collections;

public class NameText : MonoBehaviour
{
    public GUISkin guiSkin; // choose a guiStyle (Important!)

    public string text = "Player Name"; // choose your name

    public Color color = Color.white;   // choose font color/size
    public float fontSize = 30;

    public float offsetX = 0;
    public float offsetY = 0f;

    float boxW = 200f;
    float boxH = 100f;

    public bool messagePermanent = true;

    public float messageDuration { get; set; }

    Vector2 boxPosition;

    void Start()
    {
        if (messagePermanent)
        {
            messageDuration = 1;
        }
    }

    void OnGUI()
    {
        if (messageDuration > 0 && GameManager.IsState(GameState.Gameplay))
        {
            if (!messagePermanent) // if you set this to false, you can simply use this script as a popup messenger, just set messageDuration to a value above 0
            {
                messageDuration -= Time.deltaTime;
            }

            GUI.skin = guiSkin;
            boxPosition = Camera.main.WorldToScreenPoint(transform.position);
            boxPosition.y = Screen.height - boxPosition.y;
            boxPosition.x -= boxW * 0.5f;
            boxPosition.y -= boxH * 0.5f;
            guiSkin.box.fontSize = 30;

            GUI.contentColor = color;

            Vector2 content = guiSkin.box.CalcSize(new GUIContent(text));

            GUI.Box(new Rect(boxPosition.x - content.x / 2 * offsetX, boxPosition.y + offsetY, content.x*1.5f, content.y*1.5f), text);
        }
    }
}