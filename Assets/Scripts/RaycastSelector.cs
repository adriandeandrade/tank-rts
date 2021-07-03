using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RaycastSelector : MonoBehaviour
{
    public static RaycastSelector instance;

    [Header("Selector Configuration")]
    [SerializeField] private LayerMask selectableMask;
    [SerializeField] private LayerMask traverseableMask;

    [Space]

    [BoxGroup("Debug"), SerializeField, ReadOnly] private Vector3 lastPoint;

    // Private
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, selectableMask))
            {
                IUnit unit = hit.collider.GetComponent<IUnit>();
                if (unit != null)
                {
                    unit.OnSelected();
                }
            }
        }
    }

    public (Vector3 point, bool hit) GetWorldPosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, traverseableMask))
        {
            lastPoint = hit.point;
            return (hit.point, true);
        }

        return (Vector3.zero, false);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(lastPoint, 0.5f);
        }
    }
}
