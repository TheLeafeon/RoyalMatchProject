using UnityEngine;

public class CameraAgent : MonoBehaviour
{
    [SerializeField] Camera m_TargetCamera;
    [SerializeField] float m_BoardUnit;

    private void Awake()
    {
        m_TargetCamera.orthographicSize = m_BoardUnit / m_TargetCamera.aspect;
    }
}
