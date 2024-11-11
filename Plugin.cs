using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace ClassicElevator
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        const string PLUGIN_GUID = "butterystancakes.lethalcompany.classicelevator", PLUGIN_NAME = "Classic Elevator", PLUGIN_VERSION = "1.0.0";
        internal static new ManualLogSource Logger;

        void Awake()
        {
            Logger = base.Logger;

            new Harmony(PLUGIN_GUID).PatchAll();

            Logger.LogInfo($"{PLUGIN_NAME} v{PLUGIN_VERSION} loaded");
        }
    }

    [HarmonyPatch]
    class ClassicElevatorPatches
    {
        [HarmonyPatch(typeof(MineshaftElevatorController), "OnEnable")]
        [HarmonyPostfix]
        static void MineshaftElevatorControllerPostOnEnable(MineshaftElevatorController __instance)
        {
            if (System.DateTime.Now.Month == 10 && System.DateTime.Now.Day == 31)
                return;

            __instance.elevatorHalloweenClips = [__instance.elevatorJingleMusic.clip];
            __instance.elevatorHalloweenClipsLoop = __instance.elevatorHalloweenClips;
            Light neonLight = __instance.transform.Find("AnimContainer/NeonLights/Point Light")?.GetComponent<Light>();
            if (neonLight != null)
                neonLight.colorTemperature = 6217f;
        }
    }
}