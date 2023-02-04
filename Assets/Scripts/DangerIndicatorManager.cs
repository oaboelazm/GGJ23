using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DangerIndicatorManager : MonoBehaviour
{
    public static UnityEvent<Transform> OnDangerTriggered = new UnityEvent<Transform>();

    [SerializeField] DangerIndicator dangerIndicatorPrefab;
    [SerializeField] Transform dangerIndicatorsContainer;

    private Dictionary<Transform, DangerIndicator> m_DangerIndicators = new Dictionary<Transform, DangerIndicator>();
    private Camera m_Camera;

    private void Awake()
    {
        m_Camera = Camera.main;
        OnDangerTriggered.AddListener(OnDangerLocated);
    }

    private void OnDangerLocated(Transform target)
    {
        if (IsVisible(target.position)) return;
        if (m_DangerIndicators.Keys.Contains(target)) return;
        m_DangerIndicators.Add(target, Instantiate(dangerIndicatorPrefab, dangerIndicatorsContainer));
        m_DangerIndicators[target].Initialize(this,m_Camera,target);
    }

    public bool IsVisible(Vector3 point)
    {
        Vector3 targetScreenPoint = m_Camera.WorldToScreenPoint(point);
        targetScreenPoint.z = 0;
        Bounds bounds = GetCameraBounds();
        return bounds.Contains(targetScreenPoint);
    }

    public Bounds GetCameraBounds()
    {
        return new Bounds(m_Camera.WorldToScreenPoint(m_Camera.transform.position), new Vector3(Screen.width, Screen.height));
    }

    public void RemoveIndicatorFor(Transform target)
    {
        if (!m_DangerIndicators.Keys.Contains(target)) return;
        Destroy(m_DangerIndicators[target].gameObject);
        m_DangerIndicators.Remove(target);
    }
}
