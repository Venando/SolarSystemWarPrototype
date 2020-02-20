using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalObject : MonoBehaviour, ISavable
{
    public float m_MovementSpeed;
    private float m_CurrentAngle;
    private float m_OrbitalRadius;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var angleSpeed = Mathf.PI * m_MovementSpeed;

        m_CurrentAngle += angleSpeed * Time.fixedDeltaTime;

        transform.position = GetPosition(m_CurrentAngle);

        if (m_CurrentAngle > 360 * Mathf.Deg2Rad)
            m_CurrentAngle -= 360 * Mathf.Deg2Rad;
    }

    private Vector3 GetPosition(float angle)
    {
        return m_OrbitalRadius * new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
    }

    public float GetOrbitalRadius()
    {
        return m_OrbitalRadius;
    }

    public void SetPosition(float radius, float angle)
    {
        m_OrbitalRadius = radius;
        m_CurrentAngle = angle;
        transform.position = GetPosition(m_CurrentAngle);
    }

    public void SetMovementSpeed(float speed)
    {
        m_MovementSpeed = speed;
    }

    public Dictionary<string, string> GetSaveValues()
    {
        return new Dictionary<string, string>()
        {
            { nameof(m_CurrentAngle), m_CurrentAngle.ToString() },
            { nameof(m_OrbitalRadius), m_OrbitalRadius.ToString() },
            { nameof(m_MovementSpeed), m_MovementSpeed.ToString() }
        };
    }

    public void SetSavedValues(Dictionary<string, string> loadedValues)
    {
        m_CurrentAngle = float.Parse(loadedValues[nameof(m_CurrentAngle)]);
        m_OrbitalRadius = float.Parse(loadedValues[nameof(m_OrbitalRadius)]);
        m_MovementSpeed = float.Parse(loadedValues[nameof(m_MovementSpeed)]);
        SetPosition(m_OrbitalRadius, m_CurrentAngle);
    }

}
