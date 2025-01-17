using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    [SerializeField] private Agent[] agents;
    [SerializeField] private string blackboardKey = "patrol_route";

    private Transform[] points;

    private void Start()
    {
        // this behaviour does nothing by itself, it has to be
        // interacted with by the agents
        for (int i = 0; i < agents.Length; i++)
        {
            agents[i].blackboard.Set(blackboardKey, this);
        }

        points = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    public GameObject GetClosestWaypoint(Vector3 position)
    {
        Transform closest = null;
        float closestDist = Mathf.Infinity;

        for (int i = 0; i < points.Length; i++)
        {
            Transform tf = points[i];
            float dist = (tf.position - position).sqrMagnitude;
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = tf;
            }
        }

        return closest.gameObject;
    }

    public int GetIndexOfWaypoint(Transform waypoint)
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] == waypoint)
            {
                return i;
            }
        }

        Debug.LogError("Waypoint " + waypoint.name + " not found in " + transform.name);
        return -1;
    }

    public GameObject GetNextWaypoint(int currentIndex)
    {
        return points[(currentIndex + 1) % points.Length].gameObject;
    }
}
