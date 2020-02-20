using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class RocketFiring : MonoBehaviour, ISliderValueProvider, ISavable
{
    public Rocket[] RocketPrefabs;
    public Rocket RocketPrefab;

    public Transform FirePosition;

    private int m_SelectedRocket = -1;
    private float m_FireTime;
    private float m_Cooldown;
    private SphereCollider m_SphereCollider;

    private void Awake()
    {
        m_SphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        SpawnHud();
        CheckRocket();
    }

    private void SpawnHud()
    {
        if (GetComponent<PlayerController>())
            SliderSpawner.SpawnVertical(this, 14, Color.yellow);
        else
            SliderSpawner.SpawnAttached(GetComponent<Renderer>(), this, 6, Color.yellow);
    }

    private void CheckRocket()
    {
        if (m_SelectedRocket == -1)
        {
            m_SelectedRocket = UnityEngine.Random.Range(0, RocketPrefabs.Length);
            GetSelectedRocket();
        }
    }

    private void GetSelectedRocket()
    {
        RocketPrefab = RocketPrefabs[m_SelectedRocket];
    }

    public void Fire()
    {
        if (!CanFire())
            return;

        var fireDirection = FirePosition.forward;// m_OrbitalObject.GetForward();
        var rocketRenderer = RocketPrefab.GetComponentInChildren<Renderer>();
        var firePosition = FirePosition.position + fireDirection * (m_SphereCollider.radius + rocketRenderer.bounds.extents.x);
        var rocket = SavablesManager.InstantiateSavable(RocketPrefab, firePosition, Quaternion.LookRotation(fireDirection));
        rocket.SetLauncher(gameObject);
        rocket.GetComponent<GravityReceiver>().IgnoreGravityApplier = GetComponent<GravityApplier>();

        m_FireTime = Time.time;
        m_Cooldown = m_FireTime + rocket.Cooldown;
    }

    public bool CanFire()
    {
        return m_Cooldown < Time.time;
    }

    public Vector3 GetFireDirection()
    {
        return FirePosition.forward;
    }

    public float GetSliderFillPercent()
    {
        var cooldown = 1 - (m_Cooldown - Time.time) / (m_Cooldown - m_FireTime);
        return cooldown > 1 || float.IsNaN(cooldown) ? 1 : cooldown;
    }

    public Dictionary<string, string> GetSaveValues()
    {
        return new Dictionary<string, string>() 
        { 
            { "TimeTillCooldown", (m_Cooldown - Time.time).ToString() },
            { "TimeSinceFiring", (Time.time - m_FireTime).ToString() },
            { nameof(m_SelectedRocket), m_SelectedRocket.ToString() }
        };
    }

    public void SetSavedValues(Dictionary<string, string> loadedValues)
    {
        m_Cooldown = Time.time + float.Parse(loadedValues["TimeTillCooldown"]);
        m_FireTime = Time.time - float.Parse(loadedValues["TimeSinceFiring"]);
        m_SelectedRocket = int.Parse(loadedValues[nameof(m_SelectedRocket)]);
        GetSelectedRocket();
    }
}
