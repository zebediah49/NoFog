using HarmonyLib;
using UnityEngine;
using BepInEx;
using BepInEx.Logging;

namespace NoFog
{
    [BepInPlugin("org.bepinex.plugins.no_fog", "No Fog", version)]
    public class NoFog : BaseUnityPlugin
    {
        public const string version = "0.1.0";
        internal static ManualLogSource Log;

        public static Harmony harmony = new Harmony("mod.no_fog");

        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
            Log = base.Logger;
            //Log.LogInfo("Beginning Patch");

            harmony.PatchAll();
        }
    }
    [HarmonyPatch(typeof(EnvMan), nameof(EnvMan.SetEnv))]
    public static class EnvMan_SetEnv_Patch
    {
        private static void Prefix(ref EnvMan __instance, ref EnvSetup env)
        {
            env.m_fogDensityNight = 0f;
            env.m_fogDensityMorning = 0f;
            env.m_fogDensityDay = 0f;
            env.m_fogDensityEvening = 0f;
        }
    }
    [HarmonyPatch(typeof(EnvMan), nameof(EnvMan.SetParticleArrayEnabled))]
    public static class EnvMan_SetParticleArrayEnabled_Patch
    {
        private static void Postfix(ref MistEmitter __instance, GameObject[] psystems, bool enabled)
        {
            // Disable Mist clouds, does not work on Console Commands (env Misty) but should work in the regular game.
            foreach (GameObject gameObject in psystems)
            {
                MistEmitter componentInChildren = gameObject.GetComponentInChildren<MistEmitter>();
                if (componentInChildren)
                {
                    componentInChildren.enabled = false;
                }
            }
        }
    }
}
