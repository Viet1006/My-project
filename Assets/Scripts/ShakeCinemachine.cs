using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;

public class ShakeCinemachine : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera VirtualCamera;
    public static ShakeCinemachine Cinemachine;
    void Awake()
    {
        Cinemachine = this;
    }
    // Rung cam với biên độ Am, tần số Fre, thời gian Time, PosGameObj là vị trí objct gọi hàm
    public void ShakeCam(Vector2 PosGameObj,float Am, float Fre, float Time){ 
        if(IsInCameraView(PosGameObj)){
            VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Am;
            VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Fre;
            Invoke("StopShakeCam",Time);
        }
    }
    void StopShakeCam()
    {
        VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
    bool IsInCameraView(Vector2 PosCheck) // Kiểm tra vị trí được thêm vào có trong Scene không
    {
        PosCheck = Camera.main.WorldToViewportPoint(PosCheck); // Chuyển sang ViewPortPoint
        return !(PosCheck.x<0 || PosCheck.x>1 || PosCheck.y<0 || PosCheck.y>1);
    }
}
