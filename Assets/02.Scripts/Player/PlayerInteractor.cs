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
    [SerializeField] GameObject curInteractObject;
    public static Action<string, string> onInteractable;
    public static Action<ItemData> onGetItem;
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
                if (hit.collider.TryGetComponent(out IInteractable interactable) && curInteractObject != hit.collider.gameObject)
                {
                    curInteractObject = hit.collider.gameObject;

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
        if (curInteractObject == null) return;
         
        if (curInteractObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.InteractItem();

            curInteractObject = null;
            onInteractable?.Invoke(null, null);
        }
    }
}
