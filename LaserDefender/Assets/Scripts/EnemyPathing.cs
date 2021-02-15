using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;

    int checkPoint = 0;
    List<Transform> waypoints;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = LocWayPoint(0);
    }

    // Update is called once per frame
    void Update()
    {
        MoveThruPath();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void MoveThruPath()
    {
        Vector2 targetLoc;
        if (checkPoint < waypoints.Count)
        {
            targetLoc = Vector2.MoveTowards(transform.position, LocWayPoint(checkPoint), waveConfig.GetMoveSpeed() * Time.deltaTime);
            transform.position = targetLoc;
            if (transform.position.x == LocWayPoint(checkPoint).x && transform.position.y == LocWayPoint(checkPoint).y)
            {
                checkPoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector2 LocWayPoint(int wpInd)
    {
        return new Vector2(waypoints[wpInd].position.x, waypoints[wpInd].position.y);
    }
}
