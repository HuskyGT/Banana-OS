using Photon.Pun;
using Photon.Realtime;
using System.Text;
using UnityEngine;

namespace BananaOS.Pages
{
    public class ScoreboardReportMenu : WatchPage
    {
        public override string Title => "ScoreboardReportMenu";

        public override bool DisplayOnMainMenu => false;

        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 1;
        }
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("WIP");
            return stringBuilder.ToString();
            /*var content = "<color=yellow>==</color> Report User <color=yellow>==</color>\n\n";
            content += $"Report {ScoreboardPlayerMenu.viewingPlayer?.NickName.LimitLength(12)} ?\n\n\n";
            //size should be changed for this one should be smaller
            content += "Select a reason and press enter to confirm\nOr press back to stop the report";
            content += "Hate Speech\n";
            content += "Cheating\n";
            content += "Toxicity";*/
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            if (ScoreboardPlayerMenu.viewingPlayer == null)
            {
                SwitchToPage(typeof(Scoreboard));
                return;
            }
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Enter:
                    break;

                case WatchButtonType.Back:
                    ScoreboardPlayerMenu.scoreboardLine.PressButton(false, GorillaPlayerLineButton.ButtonType.Cancel);
                    SwitchToPage(typeof(ScoreboardPlayerMenu));
                    break;
            }
        }
    }
}
