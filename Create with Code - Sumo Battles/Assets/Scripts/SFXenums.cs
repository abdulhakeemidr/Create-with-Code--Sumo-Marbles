using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SFX
{
    public enum PlayerSFX
    {
        WALK,
        WALK_BRIDGE,
        JUMP,
        JUMP_LAND,
        JUMP_LAND_BRIDGE,
        PLAYER_DAMAGE,
        PICKUP,
        BUS,
        CHECKPOINT
    }

    public enum UI_SFX
    {
        BUTTON_CLICK
    }
}

[System.Serializable]
public struct SoundAssets
{
    [Header("Player SFX")]
    public AudioClip playerDamage;
    public AudioClip jumpUp, jumpLand, jumpLandBridge;
    public AudioClip walkingStep, walkingStepBridge;
    public AudioClip Pickup;
    public AudioClip BusSounds;
    public AudioClip Checkpoint;
    [Header("UI SFX")]
    public AudioClip UIButtonClick;
}
