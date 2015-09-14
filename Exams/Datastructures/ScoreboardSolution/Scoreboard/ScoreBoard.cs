using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Wintellect.PowerCollections;

class ScoreboardMain
{
    static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        var scoreboard = new ScoreBoard();

        var endCommand = string.Empty;
        while (!endCommand.Equals("End"))
        {
            string command = Console.ReadLine();
            endCommand = command.Trim();
            if (!endCommand.Equals("End") && endCommand != String.Empty)
            {
                string commandResult = scoreboard.ProcessCommand(command);
                Console.WriteLine(commandResult);
            }
        }
    }
}

public class User : IComparable<User>
{
    public string Username { get; set; }

    public string Password { get; set; }


    public int CompareTo(User other)
    {
        return this.Username.CompareTo(other.Username);
    }
}

public class Game : IComparable<Game>
{
    public string GameName { get; set; }

    public string Password { get; set; }
    public int CompareTo(Game other)
    {
        var compareResult = this.GameName.CompareTo(other.GameName);
        if (compareResult == 0)
        {
            return this.Password.CompareTo(other.Password);
        }

        return compareResult;
    }

    public override string ToString()
    {
        return $"{GameName}";
    }
}

public class ScoreBoard
{
    public const string UserRegistered = "User registered";
    public const string DuplicatedUser = "Duplicated user";

    public const string GameRegistered = "Game registered";
    public const string DuplicatedGame = "Duplicated game";

    public const string ScoreAdded = "Score added";
    public const string CannotAddScore = "Cannot add score";

    public const string NoScore = "No score";
    public const string GameNotFound = "Game not found";

    public const string NoMatches = "No matches";

    public const string GameDeleted = "Game deleted";
    public const string CannotDeleteGame = "Cannot delete game";

    public Dictionary<string, User> Users = new Dictionary<string, User>();
    public OrderedSet<string> GameNames = new OrderedSet<string>();
    public OrderedDictionary<string, Game> Games = new OrderedDictionary<string, Game>();

    public Dictionary<string, OrderedDictionary<int, OrderedBag<string>>> GameScores = new Dictionary<string, OrderedDictionary<int, OrderedBag<string>>>();

    public string RegisterUser(string username, string pasword)
    {
        if (this.Users.ContainsKey(username))
        {
            return DuplicatedUser;
        }

        this.Users.Add(username, new User
        {
            Password = pasword,
            Username = username
        });

        return UserRegistered;
    }

    public string RegisterGame(string gameName, string gamePassword)
    {
        if (this.Games.ContainsKey(gameName))
        {
            return DuplicatedGame;
        }

        this.Games.Add(gameName, new Game
        {
            Password = gamePassword,
            GameName = gameName
        });

        this.GameScores.Add(gameName, new OrderedDictionary<int, OrderedBag<string>>(Comparison));
        this.GameNames.Add(gameName);
        return GameRegistered;
    }

    private int Comparison(int first, int second)
    {
        if (first < second)
        {
            return 1;
        }
        if (first > second)
        {
            return -1;
        }

        return 0;
    }

    public string AddScore(string username, string userPassword, string gameName, string gamePassword, int score)
    {
        if (!this.Games.ContainsKey(gameName) || !this.Users.ContainsKey(username))
        {
            return CannotAddScore;
        }

        var user = this.Users[username];
        var game = this.Games[gameName];
        if (user.Password != userPassword)
        {
            return CannotAddScore;
        }

        if (game.Password != gamePassword)
        {
            return CannotAddScore;
        }

        var record = $"{user.Username} {score}";
        if (this.GameScores.ContainsKey(gameName))
        {
            if (this.GameScores[gameName].ContainsKey(score))
            {
                this.GameScores[gameName][score].Add(record);
            }
            else
            {
                this.GameScores[gameName].Add(score, new OrderedBag<string>() { { record } });
            }
        }
        else
        {
            this.GameScores.Add(gameName, new OrderedDictionary<int, OrderedBag<string>>() { { score, new OrderedBag<string>() { { record } } } });
        }

        return ScoreAdded;
    }

    public string ShowScoreboard(string gameName)
    {
        OrderedDictionary<int, OrderedBag<string>> scores;
        this.GameScores.TryGetValue(gameName, out scores);

        if (!this.Games.ContainsKey(gameName))
        {
            return GameNotFound;
        }

        if (scores.Values.Count == 0)
        {
            return NoScore;
        }

        var result = scores
            .SelectMany(p => p.Value)
            .Take(10);
        return this.PrintResult(result);
    }

    public string ListGamesByPrefix(string prefix)
    {
        //        var games = this.Games.Keys
        //            .Where(k => k.StartsWith(prefix))
        //            .Take(10);
        var games = this.GameNames
            .Range(prefix, true, prefix + "z", false)
            .Where(g => g.StartsWith(prefix))
            .Take(10);
        var result = string.Join(", ", games);
        if (result.Length == 0)
        {
            return NoMatches;
        }

        return result;
    }

    public string DeleteGame(string gameName, string gamePassword)
    {
        Game game;
        this.Games.TryGetValue(gameName, out game);

        if (game == null)
        {
            return CannotDeleteGame;
        }

        if (string.CompareOrdinal(game.Password, gamePassword) != 0)
        {
            return CannotDeleteGame;
        }

        this.Games.Remove(gameName);
        this.GameScores.Remove(gameName);
        this.GameNames.Remove(gameName);
        return GameDeleted;
    }

    private string PrintResult(IEnumerable<string> userScores)
    {
        var index = 1;
        var resultAsList = new List<string>();
        foreach (var user in userScores)
        {
            resultAsList.Add($"#{index} {user}");
            index++;
        }

        if (resultAsList.Count == 0)
        {
            return NoScore;
        }

        return string.Join("\n", resultAsList);
    }

    public string ProcessCommand(string userInput)
    {
        var command = userInput.Substring(0, userInput.IndexOf(" "));
        var commandParts = userInput.Substring(userInput.IndexOf(" ") + 1)
            .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        switch (command)
        {
            case "RegisterUser":
                return this.RegisterUser(commandParts[0], commandParts[1]);
            case "RegisterGame":
                return this.RegisterGame(commandParts[0], commandParts[1]);
            case "AddScore":
                return this.AddScore(commandParts[0], commandParts[1], commandParts[2], commandParts[3], int.Parse(commandParts[4]));
            case "ShowScoreboard":
                return this.ShowScoreboard(commandParts[0]);
            case "ListGamesByPrefix":
                return this.ListGamesByPrefix(commandParts[0]);
            case "DeleteGame":
                return this.DeleteGame(commandParts[0], commandParts[1]);
            default:
                return "Invalid command!";
        }
    }
}
