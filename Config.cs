using System.IO;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace BananaOS
{
    static internal class Config
    {
        public static ConfigFile config;
        public static ConfigEntry<Color> buttonPressColor, arrowButtonColor, enterButtonColor, backButtonColor, introColors, selectionIndicator;
        public static ConfigEntry<bool> buttonVibration, buttonSound, selectorWrapping, OnLeftHand;
        public static ConfigEntry<float> velocityCheck, closeTiltAngle, buttonPressCooldown;
        public static ConfigEntry<string> watchSkin, backgroundSkin;
        //public static ConfigEntry<bool> 

        static Config()
        {
            config = new ConfigFile(Path.Combine(Paths.ConfigPath, "BananaOS.cfg"), true);
            config.SaveOnConfigSet = true;
            RefreshSettings();
        }

        public static bool GetModActiveConfigStatus(BepInEx.PluginInfo plugin)
        {
            return config.Bind("Mod Status", plugin.Metadata.Name, true).Value;
        }
        public static bool ToggleModStatus(BepInEx.PluginInfo plugin)
        {
            var value = !plugin.Instance.enabled;
            config.Bind("Mod Status", plugin.Metadata.Name, true).Value = value;
            plugin.Instance.enabled = value;
            return value;
        }

        public static void RefreshSettings()
        {
            config.Reload();
            buttonPressColor = config.Bind("Mod Settings", "Button Press Color", new Color(104, 104, 105, 255) / 255);
            arrowButtonColor = config.Bind("Mod Settings", "Arrow Button Color", new Color(101, 168, 131, 255) / 255);
            enterButtonColor = config.Bind("Mod Settings", "Enter Button Color", new Color(184, 160, 132, 255) / 255);
            backButtonColor = config.Bind("Mod Settings", "Back Button Color", new Color(95, 108, 187) / 255);
            introColors = config.Bind("Mod Settings", "Banana OS Intro Colors", Color.yellow);
            buttonVibration = config.Bind("Mod Settings", "Button Press Vibration", true);
            buttonSound = config.Bind("Mod Settings", "Button Press Sound", true);
            selectorWrapping = config.Bind("Mod Settings", "Selector Wrapping", true);
            OnLeftHand = config.Bind("Mod Settings", "Left Handed", true);
            velocityCheck = config.Bind("Mod Settings", "Button Velocity Check Value", 6f);
            closeTiltAngle = config.Bind("Mod Settings", "Close Watch Tilt Angle", 90f);
            buttonPressCooldown = config.Bind("Mod Settings", "Button Press Cooldown", 0.1f);
            watchSkin = config.Bind("Mod Settings", "Watch Skin", "Default");
            backgroundSkin = config.Bind("Mod Settings", "Background Skin", "Default");
        }
    }
}