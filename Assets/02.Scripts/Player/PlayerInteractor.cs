using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    Camera cam;
    Player player;

    [Header("Interactor Settings")]
    [SerializeField] float interactDistance;
    [SerializeField] float checkRate;
    [SerializeField] LayerMask interactlayerMask;
    [SerializeField] IInteractable curInteractObject;
    public static Action<string, string> onInteractable;
    private float checkTimer;

    public void Init(Player player)
    {
        this.player = player;
        cam = Camera.main;
    }

    void Update()
    {
        checkTimer += Time.deltaTime;

        if (checkTimer > checkRate)
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactlayerMask))
            {
                // When Hit Interaction Object
                if (hit.collider.TryGetComponent(out IInteractable interactable) && curInteractObject != interactable)
                {
                    curInteractObject = interactable;

                    Tuple<string, string> itemInfo = interactable.GetItemInfo();
                    onInteractable?.Invoke(itemInfo.Item1, itemInfo.Item2);
                }
            }

            else
            {
                if (curInteractObject != null)
                {
                    curInteractObject = null;
                    onInteractable?.Invoke(null, null);
                }
            }

            checkTimer = 0f;
        }
    }

    public void InteractItem()
    {
        if (curInteractObject != null)
        {
            curInteractObject.InteractItem();
        }
    }
}
