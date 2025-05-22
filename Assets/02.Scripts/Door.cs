using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    Animator anim;
    private const string Name = "Screen Door";
    private const string Description = "Press E to Open";

    void Start()
    {
        anim = GetComponent<Animator>();
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

        yield return new WaitForSeconds(2f);

        anim.SetBool("IsOpen", false);
    }
}
