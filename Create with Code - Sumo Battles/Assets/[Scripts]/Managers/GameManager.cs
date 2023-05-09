using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ButtonOrientation
{
    public const int JSMoveLeft = 0;
    public const int JSMoveRight = 1;
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Touch Controls")]
    public Joystick joystickMove;
    public Joystick joystickLook;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else if(instance == null)
        {
            instance = this;
        }
    }

    public void ApplyKeyMappingPosition(int buttonOrientation)
    {
        var JSMoveTransform = joystickMove.gameObject.GetComponent<RectTransform>();
        var JSLookTransform = joystickLook.gameObject.GetComponent<RectTransform>();

        switch(buttonOrientation)
        {
            case ButtonOrientation.JSMoveLeft:
                JSMoveTransform.anchoredPosition = new Vector2(-1620.0f, JSMoveTransform.anchoredPosition.y);
                JSLookTransform.anchoredPosition = new Vector2(615.0f, JSLookTransform.anchoredPosition.y);
                break;
            case ButtonOrientation.JSMoveRight:
                JSMoveTransform.anchoredPosition = new Vector2(615.0f, JSMoveTransform.anchoredPosition.y);
                JSLookTransform.anchoredPosition = new Vector2(-1620.0f, JSLookTransform.anchoredPosition.y);
                break;
        }
    }
}
