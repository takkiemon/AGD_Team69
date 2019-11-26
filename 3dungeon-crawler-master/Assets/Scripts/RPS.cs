public enum RPSType
{
    Rock,
    Paper,
    Scissors
}
public enum RPSWinner
{
    Left,
    Right,
    Draw
}

public class RPS
{
    public static RPSWinner Play(RPSType left, RPSType right)
    {
        switch (left)
        {
            case RPSType.Rock:
                switch (right)
                {
                    case RPSType.Rock:
                        return RPSWinner.Draw;
                    case RPSType.Paper:
                        return RPSWinner.Right;
                    case RPSType.Scissors:
                        return RPSWinner.Left;
                }
                break;
            case RPSType.Paper:
                switch (right)
                {
                    case RPSType.Rock:
                        return RPSWinner.Left;
                    case RPSType.Paper:
                        return RPSWinner.Draw;
                    case RPSType.Scissors:
                        return RPSWinner.Right;
                }
                break;
            case RPSType.Scissors:
                switch (right)
                {
                    case RPSType.Rock:
                        return RPSWinner.Left;
                    case RPSType.Paper:
                        return RPSWinner.Right;
                    case RPSType.Scissors:
                        return RPSWinner.Draw;
                }
                break;
        }

        return RPSWinner.Draw;
    }
}