using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UIManager uIManager;

    protected override void Awake()
    {
        base.Awake();

        uIManager = GetComponentInChildren<UIManager>();

        if(uIManager)
            uIManager.Init(this);
    }
}
