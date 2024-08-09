using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace BananaOS.Pages
{
    public class WatchSkinPage : WatchPage
    {
        public override string Title => "Watch Skins";
        public override bool DisplayOnMainMenu => false;

        Renderer watchRenderer;
        Texture defaultTexture;
        FileInfo[] skinFiles;
        readonly string skinFolderPath = (Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Watch Skins"));
        static SelectionHandler pageSelection = new SelectionHandler();
        public static Dictionary<string, Texture> textureDict = new Dictionary<string, Texture>();
        public static Dictionary<int, List<string>> skinPageDict = new Dictionary<int, List<string>>();
        const int maxPageItemCount = 8;

        private static int CurrentPage;
        public static int currentPage
        {
            get
            {
                return CurrentPage + 1;
            }
            set
            {
                CurrentPage = value;
            }
        }
        public override void OnPostModSetup()
        {
            watchRenderer = MonkeWatch.watch.GetComponent<Renderer>();
            defaultTexture = watchRenderer.material.mainTexture;
            RefreshSkins();
            ApplySkin(Config.watchSkin.Value);
        }
        public override void OnPageOpen()
        {
            RefreshSkins();
        }
        void RefreshSkins()
        {
            skinPageDict.Clear();
            if (!skinPageDict.ContainsKey(1))
            {
                skinPageDict.Add(1, new List<string>());
                skinPageDict[1].Add("Default");
            }
            if (!Directory.Exists(skinFolderPath))
            {
                Directory.CreateDirectory(skinFolderPath);
                return;
            }
            skinFiles = new DirectoryInfo(skinFolderPath).GetFiles("*.png");
            if (skinFiles.Length == 0)
                return;

            int pageIndex = 0;
            for (int i = 0; i < skinFiles.Length; i++)
            {
                if (i % maxPageItemCount == 0)
                {
                    pageIndex++;
                    if (!skinPageDict.ContainsKey(pageIndex))
                    {
                        skinPageDict.Add(pageIndex, new List<string>());
                    }
                }
                var skin = skinFiles[i].Name.Replace(".png", "");
                if (!skinPageDict[pageIndex].Contains(skin))
                {
                    skinPageDict[pageIndex].Add(skin);
                }
            }
            pageSelection.maxIndex = skinPageDict.Count - 1;
            selectionHandler.maxIndex = skinPageDict[currentPage].Count - 1;
        }
        void ApplySkin(string skin)
        {
            Config.watchSkin.Value = skin;
            if (Config.watchSkin.Value == "Default")
            {
                watchRenderer.material.mainTexture = defaultTexture;
                return;
            }

            var skinFile = skinFiles.FirstOrDefault(file => file.Name.Replace(".png", "") == Config.watchSkin.Value);
            if (skinFile == null)
            {
                Config.watchSkin.Value = "Default";
                ApplySkin("Default");
                return;
            }

            var texture = new Texture2D(256, 256, TextureFormat.RGB24, false)
            {
                filterMode = FilterMode.Point,
            };
            texture.LoadImage(File.ReadAllBytes(Path.Combine(skinFolderPath, skin + ".png")));
            texture.Apply();
            watchRenderer.material.mainTexture = texture;
        }

        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Watch Skins <color=yellow>==</color><size=0.65>");
            stringBuilder.AppendLine("Current Skin: " + Config.watchSkin.Value.LimitLength(22));
            stringBuilder.AppendLines(1);
            var skinPage = skinPageDict[currentPage];
            for (int i = 0; i < skinPage.Count; i++)
            {
                stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(i, skinPage[i].LimitLength(30)));
            }
            stringBuilder.AppendLines(1);
            stringBuilder.AppendLine($"{currentPage}/{skinPageDict.Count}");
            return stringBuilder.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;
                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;
                case WatchButtonType.Left:
                    if (pageSelection.currentIndex == pageSelection.MoveSelectionUp())
                        return;

                    currentPage = pageSelection.currentIndex;
                    selectionHandler.maxIndex = skinPageDict[currentPage].Count - 1;
                    selectionHandler.currentIndex = 0;
                    break;
                case WatchButtonType.Right:
                    if (pageSelection.currentIndex == pageSelection.MoveSelectionDown())
                        return;

                    currentPage = pageSelection.currentIndex;
                    selectionHandler.maxIndex = skinPageDict[currentPage].Count - 1;
                    selectionHandler.currentIndex = 0;
                    break;
                case WatchButtonType.Enter:
                    var skin = skinPageDict[currentPage][selectionHandler.currentIndex];
                    ApplySkin(skin);
                    break;
                case WatchButtonType.Back:
                    SwitchToPage(typeof(SettingsPage));
                    break;
            }
        }
    }
}
