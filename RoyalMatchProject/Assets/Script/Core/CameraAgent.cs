using UnityEngine;

public class CameraAgent : MonoBehaviour
{
    [SerializeField] Camera m_TargetCamera;//크기를 설정할 대상 카메라
    [SerializeField] float m_BoardUnit; // 카메라의 넓이, 원점을 기준으로 설정, 전체 넓이가 9.2 라면 4.6을 입력

    private void Start()
    {
        //카메라의 높이 계산, 넓이x화면비율로 구할 수 있다.
        m_TargetCamera.orthographicSize = m_BoardUnit / m_TargetCamera.aspect;
    }
}
