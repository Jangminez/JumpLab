using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IInteractable
{
    Animator anim;
    NavMeshObstacle navMeshObstacle;
    private const string Name = "Screen Door";
    private const string Description = "Press E to Open";

    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    public Tuple<string, string> GetItemInfo()
    {
        Tuple<string, string> info = new Tuple<string, string>(Name, Description);
        return info;
    }

    public void InteractItem()
    {
        StopAllCoroutines();
        StartCoroutine(OpenDoorCoroutine());
    }

    IEnumerator OpenDoorCoroutine()
    {
        anim.SetBool("IsOpen", true);
        navMeshObstacle.carving = false;
        yield return null;
        
        yield return new WaitForSeconds(2f);

        anim.SetBool("IsOpen", false);
        navMeshObstacle.carving = true;
    }
}
