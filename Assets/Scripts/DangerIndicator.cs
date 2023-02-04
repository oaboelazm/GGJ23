using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DangerIndicator : MonoBehaviour
{
    public float animationSpeed = 2f;

    private DangerIndicatorManager m_DangerIndicatorManager;
    private CanvasGroup m_CanvasGroup;
    private Camera m_Camera;
    private Transform m_Target;

    private void Awake()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize(DangerIndicatorManager manager, Camera camera ,Transform target)
    {
        m_DangerIndicatorManager = manager;
        this.m_Camera = camera;
        this.m_Target = target;
        Indicate();
        AnimateDangerIcon();
    }

    public void Indicate()
    {
        StartCoroutine(KeepCheckingForTarget());

        IEnumerator KeepCheckingForTarget()
        {
            while (true)
            {
                if (m_DangerIndicatorManager.IsVisible(m_Target.position)) m_DangerIndicatorManager.RemoveIndicatorFor(m_Target);
                AdjustDangerIndicator(m_Target.position);
                yield return null;
            }
        }
    }

    private void AdjustDangerIndicator(Vector3 lookAtTarget)
    {
        Vector3 targetScreenPoint = m_Camera.WorldToScreenPoint(lookAtTarget);
        targetScreenPoint.z = 0;
        RotateTowardsDanger(targetScreenPoint);
        MoveToTheEdgeOfTheScreen(targetScreenPoint);
    }

    private void RotateTowardsDanger(Vector3 targetScreenPoint)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetScreenPoint - m_Camera.WorldToScreenPoint(m_Camera.transform.position));
    }

    private void MoveToTheEdgeOfTheScreen(Vector3 targetScreenPoint)
    {
        Bounds bounds = m_DangerIndicatorManager.GetCameraBounds();
        Vector3 closestPoint = bounds.ClosestPoint(targetScreenPoint);
        transform.position = closestPoint;
    }

    private void AnimateDangerIcon()
    {
        StartCoroutine(AnimateAlpha());

        IEnumerator AnimateAlpha()
        {
            int directionOfChange = 1;
            while (true)
            {
                if (m_CanvasGroup.alpha >= 1) directionOfChange = -1;
                if (m_CanvasGroup.alpha <= 0) directionOfChange = 1;
                m_CanvasGroup.alpha += directionOfChange * Time.deltaTime * animationSpeed;
                yield return null;
            }
        }
    }
}