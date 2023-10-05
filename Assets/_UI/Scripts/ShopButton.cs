using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image image;
    [SerializeField] GameObject frame;
    [SerializeField] GameObject selectedFrame;
    
    public void AddListen(UnityAction action)
    {
        button.onClick.AddListener(action);
    }
    public void ChangeImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void ChangeColor(Color color)
    {
        image.color = color;
    }
    public void Bought()
    {
        frame.SetActive(true);
    }
    public void ChangeState(State state)
    {
        frame.SetActive(false);
        selectedFrame.SetActive(false);
        switch (state)
        {
            case State.Bought:
                {
                    frame.SetActive(true);
                    break;
                }
            case State.UnBought:
                {

                    break;
                }
            case State.Equipped:
                {
                    selectedFrame.SetActive(true);
                    break;
                }
        }
    }
}
