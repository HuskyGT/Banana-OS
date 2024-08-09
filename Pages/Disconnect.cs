namespace BananaOS.Pages
{
    public class Disconnect : WatchPage
    {
        public override string Title => "Disconnect";

        public override bool DisplayOnMainMenu => true;

        public override string OnGetScreenContent()
        {
            NetworkSystem.Instance.ReturnToSinglePlayer();
            ReturnToMainMenu();
            return "";
        }
    }
}
