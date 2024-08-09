namespace BananaOS.Pages
{
    public class FailurePage : WatchPage
    {
        public override string Title => "Failure";
        public override bool DisplayOnMainMenu => false;

        public static string e;

        public override string OnGetScreenContent()
        {
            return "<size=0.65>" + e;
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
