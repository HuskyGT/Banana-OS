using BananaOS;
using HarmonyLib;
using System;
using UnityEngine;

namespace BananaOS
{
    [HarmonyPatch(typeof(GorillaTagger), nameof(GorillaTagger.Awake))]
    internal class InitPatch
    {
        internal static void Postfix()
        {
            try
            {
                Plugin.OnGameInitialized();
            }
            catch(Exception e)
            {
                Debug.LogError("There was an error while setting up Banana OS: " + e);
            }
        }
    }
}
