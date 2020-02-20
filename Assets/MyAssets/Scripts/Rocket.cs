using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Rocket : MonoBehaviour, IDamageable, ISavable
{
    public float Acceleration;
    public float Cooldown;
    public int RocketDamage;

    private Rigidbody m_Rigidbody;
    private GameObject m_RocketLauncher;
    private float m_AutoDestroyTime;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        AddStartSpeed();
        m_AutoDestroyTime = Time.time + 10f;
    }

    private void AddStartSpeed()
    {
        m_Rigidbody.AddForce(transform.forward * Acceleration * 150);
    }

    private void FixedUpdate()
    {
        m_Rigidbody.AddForce(transform.forward * Acceleration);
        if (m_Rigidbody.velocity.sqrMagnitude > 0f)
        {
            var angle = Mathf.Atan2(m_Rigidbody.velocity.z, -m_Rigidbody.velocity.x) * Mathf.Rad2Deg - 90f;
            transform.localEulerAngles = new Vector3(0f, angle, 0f);
        }

        if (Time.time > m_AutoDestroyTime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_RocketLauncher)
            return;

        other.GetComponent<IDamageable>()?.Damage(RocketDamage);
        Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        if (gameObject != null)
            Destroy(gameObject);
    }

    public void SetLauncher(GameObject launcherGameObject)
    {
        m_RocketLauncher = launcherGameObject;
    }

    public Dictionary<string, string> GetSaveValues()
    {
        return new Dictionary<string, string>()
        {
            { "TimeTillDestroy", (m_AutoDestroyTime - Time.time).ToString() },
        };
    }

    public void SetSavedValues(Dictionary<string, string> loadedValues)
    {
        m_AutoDestroyTime = Time.time + float.Parse(loadedValues["TimeTillDestroy"]);
    }
}
