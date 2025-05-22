using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float checkRate;
    private float time;

    public void Update()
    {
        time += Time.deltaTime;

        if (time > checkRate)
        {
            Ray ray = new Ray(start.position, start.up);

            if (Physics.Raycast(ray, out RaycastHit hit, Vector3.Distance(start.position, end.position), layerMask))
            {
                if (hit.collider.TryGetComponent(out Player player))
                {
                    player.TakeDamaged(damage);
                }
            }
            time = 0f;
        }
    }
}
