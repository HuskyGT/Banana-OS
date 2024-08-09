using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BananaOS.Pages
{
    public class SnakePage : WatchPage
    {
        public override string Title => "Snake";

        public override bool DisplayOnMainMenu => false;

        const int width = 18, height = 12;

        Vector2 snakePosition;
        int snakeLength;
        MoveDireciton facingDirection;
        List<Vector2> snakePositionHistory;

        const string snakeText = "<align=\"center\">\n<color=\"green\"> S  N  A  K  E</align>\n\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\nOOOOOOOOOOOOOOOOO\n";
        const string unfinishedText = "<align=\"center\">\n<color=\"green\"> S  N  A  K  E</align>\n\nYou have found a secret page by entering the konami code however sadly this page isn't finished at the moment come back in a later update.\n";

        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(unfinishedText);
            return stringBuilder.ToString();
        }

        public enum MoveDireciton
        {
            Up, Down, Left, Right
        }

        public void MoveSnake(MoveDireciton direction)
        {
            var newPos = snakePosition;
            switch (direction)
            {
                case MoveDireciton.Up:
                    newPos.y += 1;
                    break;

                case MoveDireciton.Down:
                    newPos.y -= 1;

                    break;

                case MoveDireciton.Right:
                    newPos.x += 1;
                    break;

                case MoveDireciton.Left:
                    newPos.x -= 1;
                    break;
            }
            if (newPos.x > width || newPos.y > height)
            {
                //boarder death
            }
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch(buttonType)
            {
                case WatchButtonType.Up:
                    facingDirection = MoveDireciton.Up;
                    break;

                case WatchButtonType.Down:
                    facingDirection = MoveDireciton.Down;
                    break;

                case WatchButtonType.Left:
                    facingDirection = MoveDireciton.Left;
                    break;

                case WatchButtonType.Right:
                    facingDirection = MoveDireciton.Right;
                    break;

                case WatchButtonType.Enter:
                    break;

                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}
