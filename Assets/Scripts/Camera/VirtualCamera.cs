using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    public enum CameraType
    {
        TPS,
        FPS
    }

    [SerializeField] private CameraType _cameraType = CameraType.TPS;

    private void Start() => CameraController.Instance.AddToVirtualCameraList(this);

    public void Toggle(CameraType cameraType) => gameObject.SetActive(_cameraType == cameraType);
}