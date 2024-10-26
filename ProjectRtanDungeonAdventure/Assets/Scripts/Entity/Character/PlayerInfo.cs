using UnityEngine;

public class PlayerInfo : CharacterInfo
{
    public StatusGauge stamina;
    public StatusPoint gem;

    public float sprintCostRate;
    public float sprintSpeed;
    public bool isOnSprint;

    public float jumpPowerOnGround;
    public float jumpPowerInAir;
    public int jumpCostOnGound;
    public int jumpCostInAir;

    public float dashSpeedOnGround;
    public float dashSpeedInAir;
    public int dashCostOnGround;
    public int dashCostInAir;
    public float dashTimeOnGround;
    public float dashTimeInAir;

    private void Awake()
    {
        PlayerManager.Instance.playerInfo = this;
    }

    protected override void Start()
    {
        base.Start();
        stamina.Initailize();
        gem.Initailize();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(isOnGround && !isOnSprint)
            stamina.Recover();

        gem.Recover();
    }
}