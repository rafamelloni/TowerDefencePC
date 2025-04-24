using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public Factory<Enemies> enemyFactory;
    public WaveManager waves;


    public Transform[] wayPoints;
    public int wave1;

    private Vector3 offset = new Vector3(0,-90,0);

   



    void Start()
    {
        enemyFactory = FindAnyObjectByType<EnemyFactory>();
        int weaveeeeeee = RemoteConfigHandler.instance.Weave1;
    }

    public override void Spawn()
    {
        var enemy = enemyFactory.Create();
        enemy.transform.position = transform.position; // Coloca el enemigo en la posición del spawner
     

        // Asegúrate de que el enemigo está activo
        enemy.gameObject.SetActive(true);

        // Asigna los waypoints al enemigo
        var followWaypoints = enemy.GetComponent<FollowWaypoints>();
        if (followWaypoints != null)
        {
            followWaypoints.SetWaypoints(wayPoints); // Pasa los waypoints al enemigo
            followWaypoints.ResetWaypoints();

            if (waves.currentWaveIndex >= 10)
            {
                followWaypoints.SetSpeed(15f);
            }
            else if (waves.currentWaveIndex >= 7)
            {
                followWaypoints.SetSpeed(10f);
            }
            else if (waves.currentWaveIndex >= 5)
            {
                followWaypoints.SetSpeed(8f);
            }
            else if (waves.currentWaveIndex >= 3)
            {
                followWaypoints.SetSpeed(6f);
            }
            else
            {
                followWaypoints.SetSpeed(3f);
            }
        }


     

    }

    public Transform[] WayPoints()
    {
        return wayPoints;
    }

   
    private void OnDrawGizmos()
    {
        if (wayPoints.Length > 0)
        {
            Gizmos.color = Color.red; // Color de la línea
            for (int i = 0; i < wayPoints.Length - 1; i++)
            {
                Gizmos.DrawLine(wayPoints[i].position, wayPoints[i + 1].position);
                Gizmos.DrawSphere(wayPoints[i].position, 0.1f); // Dibuja un marcador en cada waypoint
            }
            // Dibuja un marcador en el último waypoint
            Gizmos.DrawSphere(wayPoints[wayPoints.Length - 1].position, 0.1f);
        }
    }
}

