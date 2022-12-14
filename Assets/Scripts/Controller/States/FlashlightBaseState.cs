using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlashlightBaseState
{
    protected FlashlightController controller;

    public FlashlightBaseState(FlashlightController controller)
    {
        this.controller = controller;
    }

    public abstract void UseFlashlight();
}
