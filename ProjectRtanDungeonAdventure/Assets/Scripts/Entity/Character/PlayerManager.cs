using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : GenericSingleton<PlayerManager>
{
    public PlayerInfo playerInfo;
    public ItemData curItem = null;
}