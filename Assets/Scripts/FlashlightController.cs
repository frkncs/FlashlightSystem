using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    #region Variables
    
    // Public Variables
    
    // Private Variables
    [SerializeField] private KeyCode useFlashlightKey;
    [SerializeField] private Transform deactiveFlashlightTargetTransform;
    [SerializeField] private Transform activeFlashlightTargetTransform;
    [SerializeField] private float animationSpeed = 12;

    private Light _light;
    private FlashlightBaseState _currentState;

    private Coroutine _flashlightAnimCor;

    #endregion Variables

    private void Awake()
    {
        _light = GetComponentInChildren<Light>();
        
        ChangeState(new FlashlightDeactiveState(this));
    }

    private void Update()
    {
        if (Input.GetKeyDown(useFlashlightKey))
        {
            _currentState.UseFlashlight();
        }
    }

    public void ActivateFlashlight()
    {
        if (_flashlightAnimCor != null)
        {
            StopCoroutine(_flashlightAnimCor);
        }
        
        _light.enabled = true;
        PlayActivateAnim();
    }

    public void DeactivateFlashlight()
    {
        if (_flashlightAnimCor != null)
        {
            StopCoroutine(_flashlightAnimCor);
        }
        
        _light.enabled = false;
        PlayDeactivateAnim();
    }
    
    public void ChangeState(FlashlightBaseState newState) => _currentState = newState;

    private void PlayActivateAnim()
    {
        _flashlightAnimCor =
            StartCoroutine(SetTransformToTargetTransform(transform, activeFlashlightTargetTransform, animationSpeed));
    }
    
    private void PlayDeactivateAnim()
    {
        _flashlightAnimCor =
            StartCoroutine(SetTransformToTargetTransform(transform, deactiveFlashlightTargetTransform, animationSpeed));
    }

    private IEnumerator SetTransformToTargetTransform(Transform transformToChange, Transform targetTransform, float speed)
    {
        Vector3 currentPosition = transformToChange.position;
        Quaternion currentRotation = transformToChange.rotation;

        Vector3 targetPos = targetTransform.position;
        Quaternion targetRot = targetTransform.rotation;

        float timeCount = 0;
        
        while (true)
        {
            transformToChange.position = Vector3.Lerp(currentPosition, targetPos, timeCount);
            transformToChange.rotation = Quaternion.Slerp(currentRotation, targetRot, timeCount);

            timeCount += Time.deltaTime * speed;

            if (transformToChange.rotation == targetRot &&
                transformToChange.position == targetPos)
            {
                break;
            }
            
            yield return null;
        }
    }
}
