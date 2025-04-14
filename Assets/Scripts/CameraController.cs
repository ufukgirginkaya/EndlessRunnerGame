using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineCamera cinemachineCamera;
    [SerializeField] float minFOV = 40f;
    [SerializeField] float maxFOV = 90f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 5f;

    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));
    }

    IEnumerator ChangeFOVRoutine(float speedAmount) //Lerp ***
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + speedAmount*zoomSpeedModifier, minFOV, maxFOV);

        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV;


        
    }
}
