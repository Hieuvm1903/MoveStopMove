using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenIndicator : MonoBehaviour
{
    [Range(0.5f, 1f)]
    [Tooltip("Distance offset of the indicators from the centre of the screen")]
    [SerializeField] private float screenBoundOffset = 0.9f;
    public static Transform tf;
    private Camera mainCamera;
    private Vector3 screenCentre;
    private Vector3 screenBounds;
    void Awake()
    {
        
        mainCamera = Camera.main;
        screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
        screenBounds = screenCentre * screenBoundOffset;

    }
    private void Start()
    {
        tf = transform;
    }
    private void LateUpdate()
    {
        DrawIndicators();
    }

    void DrawIndicators()
    {
        foreach (Enemy target in LevelManager.Instance.enemies)
        {
            Vector3 screenPosition = GetScreenPosition(mainCamera, target.transform.position);
            bool isTargetVisible = IsTargetVisible(screenPosition);
            Indicator indicator = target.indicator;
            if ( !isTargetVisible && GameManager.IsState(GameState.Gameplay))
            {
                indicator.gameObject.SetActive(true);
                float angle = float.MinValue;
                GetArrowIndicatorPositionAndAngle(ref screenPosition,ref angle,screenCentre,screenBounds);
                indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg-90); // Sets the rotation for the arrow indicator.                        
                indicator.transform.position = screenPosition; //Sets the position of the indicator on the screen.
                indicator.SetTextRotationAndPosition(Quaternion.identity,indicator.transform.position);
                

            }
            else
            {
                indicator.gameObject.SetActive(false);
            }
        }
    }


    public static Vector3 GetScreenPosition(Camera mainCamera, Vector3 targetPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetPosition);
        return screenPosition;
    }
    public static bool IsTargetVisible(Vector3 screenPosition)
    {
        bool isTargetVisible = screenPosition.z > 0 && screenPosition.x > 0 && screenPosition.x < Screen.width && screenPosition.y > 0 && screenPosition.y < Screen.height;
        return isTargetVisible;
    }
    public static void GetArrowIndicatorPositionAndAngle(ref Vector3 screenPosition, ref float angle, Vector3 screenCentre, Vector3 screenBounds)
    {
        screenPosition -= screenCentre;
        if (screenPosition.z < 0)
        {
            screenPosition *= -1;
        }
        angle = Mathf.Atan2(screenPosition.y, screenPosition.x);       
        float slope = Mathf.Tan(angle);
        if (screenPosition.x > 0)
        {          
            screenPosition = new Vector3(screenBounds.x, screenBounds.x * slope, 0);
        }
        else
        {
            screenPosition = new Vector3(-screenBounds.x, -screenBounds.x * slope, 0);
        }       
        if (screenPosition.y > screenBounds.y)
        {         
            screenPosition = new Vector3(screenBounds.y / slope, screenBounds.y, 0);
        }
        else if (screenPosition.y < -screenBounds.y)
        {
            screenPosition = new Vector3(-screenBounds.y / slope, -screenBounds.y, 0);
        }
        // Bring the ScreenPosition back to its original reference.
        screenPosition += screenCentre;
    }





}
