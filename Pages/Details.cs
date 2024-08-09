using GorillaNetworking;
using Photon.Pun;
using System;
using System.Text;
using UnityEngine;

namespace BananaOS.Pages
{
    public class Details : WatchPage
    {
        public override string Title => "Details";

        public override bool DisplayOnMainMenu => true;

        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<color=yellow>==</color> Details <color=yellow>==</color>");
            stringBuilder.AppendLine("  <size=0.40>Refresh by reopening this menu</size>");
            stringBuilder.AppendLines(1);
            stringBuilder.AppendLine($"<size=0.55>Current Time:\n{DateTime.Now.ToString("ddd MMM d HH:mm:ss yyyy")}");
            stringBuilder.AppendLines(1);
            stringBuilder.AppendLine("Current Game Version:");
            stringBuilder.AppendLine(GorillaComputer.instance.version);
            stringBuilder.AppendLine("Current Name:");
            stringBuilder.AppendLine(PlayerPrefs.GetString("playerName"));
            stringBuilder.AppendLines(1);

            if (PhotonNetwork.InRoom)
            {
                stringBuilder.AppendLine("Current Room:");
                stringBuilder.AppendLine(PhotonNetwork.CurrentRoom.Name);
                stringBuilder.AppendLine("Players In Room:");
                stringBuilder.AppendLine(PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            }
            else
            {
                stringBuilder.AppendLine("Not in Room");
            }
            stringBuilder.AppendLine("Players Online:");
            stringBuilder.AppendLine(NetworkSystem.Instance.GlobalPlayerCount().ToString() + "</size>");
            return stringBuilder.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}
