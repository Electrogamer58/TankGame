using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Window_BossPointer : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    [SerializeField] Transform target;
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    private void Awake()
    {
        targetPosition = target.transform.position;
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        //targetPosition = target.transform.position;
    }

    private void Update()
    {
        //targetPosition = target.transform.position;
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
        
        if (isOffScreen)
        {
            Vector3 cappedTargetScreenPos = targetPositionScreenPoint;
            if (cappedTargetScreenPos.x <= 0) cappedTargetScreenPos.x = 0f;
            if (cappedTargetScreenPos.x >= Screen.width) cappedTargetScreenPos.x = Screen.width;
            if (cappedTargetScreenPos.y <= 0) cappedTargetScreenPos.y = 0f;
            if (cappedTargetScreenPos.y >= Screen.height) cappedTargetScreenPos.y = Screen.height;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPos);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
    }

}
