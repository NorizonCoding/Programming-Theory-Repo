using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    protected float m_Speed;

    protected int m_Year;

    public int Year {  get { return m_Year; } set { if (value >= 0) m_Year = value; } }
    public float Speed { get { return m_Speed; } set { if (value >= 0) m_Speed = value; } }

    protected bool engineOn = false;

    protected Rigidbody rb;

    public virtual void TurnOn() {  engineOn = true; }
    public virtual void TurnOff() { engineOn = false; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public abstract void Move();
}
