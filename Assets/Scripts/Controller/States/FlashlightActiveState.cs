using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightActiveState : FlashlightBaseState
{
    public FlashlightActiveState(FlashlightController controller) : base(controller)
    {
        controller.ActivateFlashlight();
    }

    public override void UseFlashlight()
    {
        controller.ChangeState(new FlashlightDeactiveState(controller));
    }
}
