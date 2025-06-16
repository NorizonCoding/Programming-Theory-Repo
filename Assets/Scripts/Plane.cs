using UnityEngine;

public class Plane : Vehicle
{
    public override void Move()
    {
        if (engineOn)
        {

        }
    }

    public override void TurnOn()
    {
        base.TurnOn();
        rb.useGravity = false;
    }

    public override void TurnOff()
    {
        base.TurnOff();
        rb.useGravity = true;
    }
}
