using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(RocketFiring))]
public class AiController : MonoBehaviour, IController
{
    private const float ANGLE_PRECEPTION = 15f;

    private RocketFiring m_RocketFiring;
    private List<Transform> m_Enemies;

    private void Awake()
    {
        m_RocketFiring = GetComponent<RocketFiring>();
    }

    private void Start()
    {
        m_Enemies = FindObjectsOfType<PlanetHealth>()
            .Select(h => h.transform)
            .Where(t => t != transform)
            .ToList();

        StartCoroutine(Firing());
    }

    private IEnumerator Firing()
    {
        while (true)
        {
            yield return null;

            if (!m_RocketFiring.CanFire())
                continue;

            if (IsAnyEnemyInFront(m_RocketFiring.GetFireDirection()))
            {
                yield return new WaitForSeconds(Random.Range(0f, 0.05f));
                m_RocketFiring.Fire();
            }
        }
    }

    private bool IsAnyEnemyInFront(Vector3 fireDirection)
    {

        for (int i = m_Enemies.Count - 1; i > -1; i--)
        {
            var enemy = m_Enemies[i];

            if (enemy == null)
            {
                m_Enemies.RemoveAt(i);
                continue;
            }

            var vectorToEnemy = (enemy.transform.position - transform.position);

            var directionToEnemy = vectorToEnemy.normalized;

            var angle = Mathf.Acos(Vector3.Dot(directionToEnemy, fireDirection)) * Mathf.Rad2Deg;

            var distanceToEnemy = vectorToEnemy.magnitude;

            var adjustedAnglePreception = ANGLE_PRECEPTION - Mathf.Lerp(5f, 0f, distanceToEnemy / 8f);

            if (angle < ANGLE_PRECEPTION)
            {
                return true;
            }
        }

        return false;
    }
}
