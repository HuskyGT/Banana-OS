using GorillaNetworking;
using Photon.Pun;
using System;
using TMPro;
using UnityEngine;

namespace BananaOS.Pages
{
    public class IntroPage : WatchPage
    {
        const string introString = "<align=\"center\">\n\n\n\n\n\n\n\n\nB A N A N A   <color=\"white\">O S</align>";
        Renderer bananaIconRenderer;
        TMP_Text screenText;

        public override string Title => "Intro";

        public override bool DisplayOnMainMenu => false;

        public override void OnPostModSetup()
        {
            bananaIconRenderer = MonkeWatch.Instance.bananaIcon.GetComponent<Renderer>();
            screenText = MonkeWatch.Instance.screenText;
        }

        public override void OnPageOpen()
        {
            MonkeWatch.Instance.bananaIcon.SetActive(true);
            bananaIconRenderer.material.color = Config.introColors.Value;
            screenText.color = Config.introColors.Value;
        }

        public override string OnGetScreenContent()
        {
            return introString;
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            MonkeWatch.Instance.bananaIcon.SetActive(false);
            MonkeWatch.Instance.screenText.color = Color.white;
            if (UpdateModPage.IsNewestVersion)
            {
                ReturnToMainMenu();
                return;
            }
            SwitchToPage(typeof(UpdateModPage));
        }
    }
}
