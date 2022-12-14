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
        _flashlightAnimCor = StartCoroutine(PlayActivateAnim());
    }

    public void DeactivateFlashlight()
    {
        if (_flashlightAnimCor != null)
        {
            StopCoroutine(_flashlightAnimCor);
        }
        
        _light.enabled = false;
        _flashlightAnimCor = StartCoroutine(PlayDeactivateAnim());
    }
    
    public void ChangeState(FlashlightBaseState newState) => _currentState = newState;

    private IEnumerator PlayActivateAnim()
    {
        while (true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 12 * Time.deltaTime);

            if (transform.rotation == Quaternion.identity)
            {
                break;
            }
            
            yield return null;
        }
    }

    private IEnumerator PlayDeactivateAnim()
    {
        yield return null;
    }
}
