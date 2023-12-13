using System;
using System.Linq;

class Program
{
    static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static int currentPlayer = 1; // 1 for player X, 2 for player O
    static bool isGameActive = true;
    static UserInfo userInfo = new UserInfo();

    static void Main()
    {
        GetUserInfo();

        do
        {
            Console.Clear();
            Console.WriteLine("1. New game");
            Console.WriteLine("2. About the author");
            Console.WriteLine("3. Exit");
            Console.Write("> ");
            string menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    StartGame();
                    break;
                case "2":
                    DisplayAuthorInfo();
                    break;
                case "3":
                    ExitGame();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        } while (true);
    }

    static void GetUserInfo()
    {
        Console.WriteLine("Please provide your information:");
        Console.Write("First Name: ");
        userInfo.FirstName = Console.ReadLine();
        Console.Write("Last Name: ");
        userInfo.LastName = Console.ReadLine();
        Console.Write("Street: ");
        userInfo.Street = Console.ReadLine();
        Console.Write("House Number: ");
        userInfo.HouseNumber = Console.ReadLine();
        Console.Write("Flat Number: ");
        userInfo.FlatNumber = Console.ReadLine();
        Console.Write("Zip Code: ");
        userInfo.ZipCode = Console.ReadLine();
        Console.Write("City: ");
        userInfo.City = Console.ReadLine();
    }

    static void StartGame()
    {
        InitializeBoard();

        do
        {
            Console.Clear();
            Console.WriteLine(" | | ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");

            HandleMove();
            CheckForWinner();
            SwitchPlayer();

        } while (isGameActive);

        Console.ReadLine(); // Keep console window open after the game ends
    }

    static void DisplayAuthorInfo()
    {
        Console.Clear();
        Console.WriteLine($"{userInfo.FirstName} {userInfo.LastName}");
        Console.WriteLine($"St. {userInfo.Street} {userInfo.HouseNumber}/{userInfo.FlatNumber}");
        Console.WriteLine($"{userInfo.ZipCode} {userInfo.City}");
        Console.WriteLine("[Press Enter to return to main menu...]");
        Console.ReadLine();
    }

    static void ExitGame()
    {
        Console.Write("Are you sure you want to quit? [y/n] ");
        string response = Console.ReadLine();

        if (response.ToLower() == "y")
        {
            Environment.Exit(0);
        }
    }

    static void InitializeBoard()
    {
        board = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        currentPlayer = 1;
        isGameActive = true;
    }

    static void HandleMove()
    {
        bool isValidMove = false;
        int move;

        do
        {
            Console.Write($"{GetPlayerSymbol()}'s move > ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out move) && move >= 1 && move <= 9 && board[move - 1] != 'X' && board[move - 1] != 'O')
            {
                isValidMove = true;
            }
            else
            {
                Console.WriteLine("Illegal move! Try again.");
            }

        } while (!isValidMove);

        board[move - 1] = GetPlayerSymbol();
    }

    static char GetPlayerSymbol()
    {
        return currentPlayer == 1 ? 'X' : 'O';
    }

    static void SwitchPlayer()
    {
        currentPlayer = currentPlayer == 1 ? 2 : 1;
    }

    static void CheckForWinner()
    {
        if (CheckForWin('X'))
        {
            Console.Clear();
            DrawBoard();
            Console.WriteLine("Player X wins!");
            isGameActive = false;
            PromptToReturnToMenu();
        }
        else if (CheckForWin('O'))
        {
            Console.Clear();
            DrawBoard();
            Console.WriteLine("Player O wins!");
            isGameActive = false;
            PromptToReturnToMenu();
        }
        else if (board.All(cell => cell == 'X' || cell == 'O'))
        {
            Console.Clear();
            DrawBoard();
            Console.WriteLine("It's a draw!");
            isGameActive = false;
            PromptToReturnToMenu();
        }
    }

    static void DrawBoard()
    {
        Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
    }

    static bool CheckForWin(char symbol)
    {
        return (board[0] == symbol && board[1] == symbol && board[2] == symbol) ||
               (board[3] == symbol && board[4] == symbol && board[5] == symbol) ||
               (board[6] == symbol && board[7] == symbol && board[8] == symbol) ||
               (board[0] == symbol && board[3] == symbol && board[6] == symbol) ||
               (board[1] == symbol && board[4] == symbol && board[7] == symbol) ||
               (board[2] == symbol && board[5] == symbol && board[8] == symbol) ||
               (board[0] == symbol && board[4] == symbol && board[8] == symbol) ||
               (board[2] == symbol && board[4] == symbol && board[6] == symbol);
    }

    static void PromptToReturnToMenu()
    {
        Console.WriteLine("[Press Enter to return to main menu...]");
        Console.ReadLine();
    }
}

class UserInfo
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string FlatNumber { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
}