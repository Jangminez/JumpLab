using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    Camera cam;
    Player player;

    [Header("Interactor Settings")]
    [SerializeField] Transform rayStartTr; 
    [SerializeField] float interactDistance;
    [SerializeField] float checkRate;
    [SerializeField] LayerMask interactlayerMask;
    [SerializeField] GameObject curInteractObject;
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
            Ray ray = new Ray(rayStartTr.position, rayStartTr.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactlayerMask))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable) && curInteractObject != hit.collider.gameObject)
                {
                    curInteractObject = hit.collider.gameObject;

                    Tuple<string, string> itemInfo = interactable.GetItemInfo();
                    player.Events.RaisedInteracted(itemInfo.Item1, itemInfo.Item2);
                }
            }

            else
            {
                if (curInteractObject != null)
                {
                    curInteractObject = null;
                    player.Events.RaisedInteracted(null, null);
                }
            }

            checkTimer = 0f;
        }
    }

    public void GetItem()
    {
        if (curInteractObject == null) return;

        if (curInteractObject.TryGetComponent(out IInteractable interactable))
        {
            if (curInteractObject.TryGetComponent(out ItemObject obj))
                player.Events.RasiedGetItem(obj.dataSO);

            interactable.InteractItem();

            curInteractObject = null;
            player.Events.RaisedInteracted(null, null);
        }
    }
}
