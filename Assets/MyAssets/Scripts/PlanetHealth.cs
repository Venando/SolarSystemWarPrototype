using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHealth : MonoBehaviour, IDamageable, ISliderValueProvider, ISavable
{
    const int MaxHealth = 250;
    public int Health { get; private set; } = MaxHealth;

    private void Start()
    {
        if (GetComponent<PlayerController>())
            SliderSpawner.SpawnVertical(this, 0, Color.green);
        else
            SliderSpawner.SpawnAttached(GetComponent<Renderer>(), this, 14, Color.green);
    }

    public void Damage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetSliderFillPercent()
    {
        return Health / (float)MaxHealth;
    }

    public Dictionary<string, string> GetSaveValues()
    {
        return new Dictionary<string, string>() { { nameof(Health), Health.ToString() } };
    }

    public void SetSavedValues(Dictionary<string, string> loadedValues)
    {
        Health = int.Parse(loadedValues[nameof(Health)]);
    }
}
