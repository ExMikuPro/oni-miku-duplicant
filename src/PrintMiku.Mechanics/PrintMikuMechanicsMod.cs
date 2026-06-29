using HarmonyLib;
using KMod;
using UnityEngine;

namespace PrintMiku.Mechanics;

public sealed class PrintMikuMechanicsMod : UserMod2
{
    public override void OnLoad(Harmony harmony)
    {
        base.OnLoad(harmony);
        Debug.Log("[PrintMiku.Mechanics] Mechanics patches loaded.");
    }
}
