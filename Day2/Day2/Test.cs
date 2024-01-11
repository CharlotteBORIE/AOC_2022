namespace Day2;

public class Test
{
    public string[] matches;
    public int[] scores;
    public enum Played
    {
        Rock,
        Paper,
        Scissors,
    }
    
    public enum Result
    {
        Win,
        Lose,
        Draw,
    }

    public int GetScore(Played play)
    {
        Console.WriteLine(play);
        switch (play)
        {
            case Played.Rock:
                return 1;
            case Played.Paper:
                return 2;
            case Played.Scissors:
                return 3;
            default:
                throw new Exception("AIE");
        }
    }
    
    public int GetScore(Played play,Played opponent)
    {
        switch (play-opponent)
        {
            case 0:
                return 3;
            case -1 :
            case 2 :
                return 0;
            case 1 :
            case -2:
                return 6;
            default:
                throw new Exception("AIE");
        }
    }
    
    public int GetPlay(Played opponent,Result play)
    {
        Console.Write(play+" ");
        switch (play)
        {
            case Result.Draw:
                return GetScore(opponent)+3;
            case Result.Lose:
                return GetScore((Played) (((int) (opponent + 2)) % 3));
           case Result.Win:
                return 6+GetScore((Played) (((int) (opponent + 1)) % 3));
            default:
                throw new Exception("AIE");
        }
    }

    public void ReadFile(string filepath)
    {
        matches = System.IO.File.ReadAllLines(filepath);
    }

    public void EvaluateScores()
    {
        scores = new int[matches.Length];
        int index=0;
        foreach (var info in matches)
        {
            Played opponentString;
            switch (info[0])
            {
                case 'A':
                    opponentString = Played.Rock;
                    break;
                case 'B':
                    opponentString = Played.Paper;
                    break;
                case 'C':
                    opponentString = Played.Scissors;
                    break;
                default:
                    throw new Exception("Help2");
            }
            
            Played myString;
            switch (info[2])
            {
                case 'X':
                    myString = Played.Rock;
                    break;
                case 'Y':
                    myString = Played.Paper;
                    break;
                case 'Z':
                    myString = Played.Scissors;
                    break;
                default:
                    throw new Exception("Help1");
            }

            scores[index] = GetScore(myString);
                            scores[index]+= GetScore(myString, opponentString);
                
                index++;
        }
    }
    
    public void EvaluateNewScores()
    {
        scores = new int[matches.Length];
        int index=0;
        foreach (var info in matches)
        {
            Played opponentString;
            switch (info[0])
            {
                case 'A':
                    opponentString = Played.Rock;
                    break;
                case 'B':
                    opponentString = Played.Paper;
                    break;
                case 'C':
                    opponentString = Played.Scissors;
                    break;
                default:
                    throw new Exception("Help2");
            }
            
            Result myString;
            switch (info[2])
            {
                case 'X':
                    myString = Result.Lose;
                    break;
                case 'Y':
                    myString = Result.Draw;
                    break;
                case 'Z':
                    myString = Result.Win;
                    break;
                default:
                    throw new Exception("Help1");
            }
            scores[index]+= GetPlay( opponentString, myString);
                
            index++;
        }
    }

    public void Exo1(string path)
    {
        ReadFile(path);
        EvaluateScores();
        Console.WriteLine(scores.Sum());
    }

    public void Exo2(string path)
    {
        ReadFile(path);
        EvaluateNewScores();
        Console.WriteLine(scores.Sum());
    }
}