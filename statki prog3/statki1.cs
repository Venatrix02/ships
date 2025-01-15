using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Game game = new Game();
        game.Start();
    }
}

public class Game
{
    private Board playerBoard;
    private Board enemyBoard;

    public Game()
    {
        playerBoard = new Board();
        enemyBoard = new Board();
        enemyBoard.PlaceShipsRandomly();
    }

    public void Start()
    {
        Console.WriteLine("Gra w statki!");
        while (!enemyBoard.AllShipsSunk())
        {
            Console.WriteLine("Twoja plansza:");
            playerBoard.Display();
            Console.WriteLine("Plansza przeciwnika:");
            enemyBoard.Display();

            Console.Write("Podaj współrzędne (np. A1): ");
            string input = Console.ReadLine();
            if (enemyBoard.Attack(input))
                Console.WriteLine("Trafiony!");
            else
                Console.WriteLine("Pudło!");
        }
        Console.WriteLine("Gratulacje! Zniszczyłeś wszystkie statki!");
    }
}

public class Board
{
    private const int Size = 5;
    private char[,] grid;
    private List<Ship> ships;

    public Board()
    {
        grid = new char[Size, Size];
        ships = new List<Ship>();
        for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
                grid[i, j] = '-';
    }

    public void PlaceShipsRandomly()
    {
        ships.Add(new Ship(2, 0, 0, true));
        ships.Add(new Ship(3, 2, 2, false));
        foreach (var ship in ships)
        {
            for (int i = 0; i < ship.Length; i++)
            {
                int x = ship.Horizontal ? ship.X + i : ship.X;
                int y = ship.Horizontal ? ship.Y : ship.Y + i;
                grid[x, y] = 'o';
            }
        }
    }

    public bool Attack(string position)
    {
        int x = position[1] - '1';
        int y = position[0] - 'A';
        if (grid[x, y] == 'o')
        {
            grid[x, y] = 't';
            return true;
        }
        else if (grid[x, y] == 'S')
        {
            grid[x, y] = 'x';
            return true;
        }
        grid[x, y] = 'O';
        return false;
    }

    public bool AllShipsSunk()
    {
        foreach (var ship in ships)
        {
            for (int i = 0; i < ship.Length; i++)
            {
                int x = ship.Horizontal ? ship.X + i : ship.X;
                int y = ship.Horizontal ? ship.Y : ship.Y + i;
                if (grid[x, y] == 'o') return false;
            }
        }
        return true;
    }

    public void Display()
    {
        Console.WriteLine("  A B C D E");
        for (int i = 0; i < Size; i++)
        {
            Console.Write(i + 1 + " ");
            for (int j = 0; j < Size; j++)
                Console.Write(grid[i, j] + " ");
            Console.WriteLine();
        }
    }
}

public class Ship
{
    public int Length { get; }
    public int X { get; }
    public int Y { get; }
    public bool Horizontal { get; }

    public Ship(int length, int x, int y, bool horizontal)
    {
        Length = length;
        X = x;
        Y = y;
        Horizontal = horizontal;
    }
}
