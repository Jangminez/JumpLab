using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    Camera cam;

    [SerializeField] float interactDistance;
    [SerializeField] LayerMask interactlayerMask;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactlayerMask))
        {
            // When Hit Interaction Object
            
        }
    }

}
