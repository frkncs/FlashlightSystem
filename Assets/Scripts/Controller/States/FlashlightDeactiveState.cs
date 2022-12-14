using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightDeactiveState : FlashlightBaseState
{
    public FlashlightDeactiveState(FlashlightController controller) : base(controller)
    {
        controller.DeactivateFlashlight();
    }

    public override void UseFlashlight()
    {
        controller.ChangeState(new FlashlightActiveState(controller));
    }
}
