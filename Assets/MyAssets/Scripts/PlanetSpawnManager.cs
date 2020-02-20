using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanetSpawnManager : MonoBehaviour
{
    public GameObject PlanetPrefab;

    public float DistanceBetweenPlanets;
    public float MinOrbitalRadius;
    public Vector2 MinMaxMoveSpeed;
    public Vector2Int MinMaxAiPlayersNumber;

    private void Awake()
    {
        if (SaveManager.IsSaveLoading())
        {
            SaveManager.GameLoaded();
            SavablesManager.LoadSave(SaveManager.GetSave());
        }
        else
        {
            Spawn();
        }

        SetupCameraHeight();
    }

    public void Spawn()
    {
        var numberOfPlanes = Random.Range(MinMaxAiPlayersNumber.x, MinMaxAiPlayersNumber.y + 1);//(int)(spawnZone / DistanceBetweenPlanets);
        var humanPlayerPlanetIndex = Random.Range(0, numberOfPlanes);

        for (int i = 0; i < numberOfPlanes; i++)
        {
            var spawnRadius = MinOrbitalRadius + i * DistanceBetweenPlanets;
            var spawnAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            var planet = SavablesManager.InstantiateSavable(PlanetPrefab);

            var rigidbody = planet.GetComponent<Rigidbody>();

            rigidbody.angularVelocity = new Vector3(0f, 4f, 0f) * (Random.value > 0.5 ? -1f : 1f);
            planet.transform.localEulerAngles = new Vector3(0f, Random.Range(0f, 360f), 0f);

            var orbitalObject = planet.GetComponent<OrbitalObject>();

            orbitalObject.SetMovementSpeed(Random.Range(MinMaxMoveSpeed.x, MinMaxMoveSpeed.y));
            orbitalObject.SetPosition(spawnRadius, spawnAngle);

            if (i == humanPlayerPlanetIndex)
                planet.AddComponent<PlayerController>();
            else
                planet.AddComponent<AiController>();
        }

    }

    private void SetupCameraHeight()
    {
        var maxRadius = FindObjectsOfType<OrbitalObject>().Max(o => o.GetOrbitalRadius()) + DistanceBetweenPlanets;
        var height = Mathf.Sin(60 * Mathf.Deg2Rad) * maxRadius / Mathf.Sin(30 * Mathf.Deg2Rad);
        Camera.main.transform.position = Vector3.up * height;
    }
}
