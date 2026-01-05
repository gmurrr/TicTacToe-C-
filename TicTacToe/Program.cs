using System.Security.AccessControl;

public class Player {
    public string Name { get; set; }
    public char Mark { get; set; }

    public Player(string name, char mark)
    {
        Name = name;
        Mark = mark;
    }
}

public class Board { 
    private char[,] field = new char[3, 3];

    public Board() { 
        Initialize();
    }
    private void Initialize()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                field[i, j] = ' ';
            }
        }
    }

    public void Display()
    {
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(" {0} | {1} | {2} ", field[i, 0], field[i, 1], field[i, 2]);
            if (i < 2)
                Console.WriteLine("---|---|---");
        }
    }

    public bool MakeMove(int fieldIndex, char playersMark) { 
        if (fieldIndex < 1 || fieldIndex > 9)
            return false;

        int index0 = fieldIndex - 1;
        int row = index0 / 3;
        int col = index0 % 3;

        if (field[row, col] != 'X' && field[row, col] != 'O') {
            field[row, col] = playersMark;
            return true;
        }
        return false;
    }

    public bool HasWinner(char mark) {
        // Kradziony algorytm sprawdzający od innego programisty :D
        // rows
        for (int r = 0; r < 3; r++)
        {
            if (field[r, 0] == mark && field[r, 1] == mark && field[r, 2] == mark)
                return true;
        }

        // columns
        for (int c = 0; c < 3; c++)
        {
            if (field[0, c] == mark && field[1, c] == mark && field[2, c] == mark)
                return true;
        }

        // diagonals
        if (field[0, 0] == mark && field[1, 1] == mark && field[2, 2] == mark)
            return true;
        if (field[0, 2] == mark && field[1, 1] == mark && field[2, 0] == mark)
            return true;

        return false;
    }

    public bool IsFull() {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (field[i, j] == ' ')
                    return false;
        return true;
    }
}

class Game {
    static void Main() { 
        Board board = new Board();
        Player playerOne = new Player ("Player 1", 'X');
        Player playerTwo = new Player ("Player 2", 'O');

        Player[] players = { playerOne, playerTwo };
        int current = 0;

        while (true) {
            Console.Clear();
            Console.WriteLine("RULES: Board is numerated form 1-9 starting from top left.");
            board.Display();
            Console.WriteLine();
            Console.WriteLine("{0}'s turn. Enter field number (1-9): ", players[current].Name);

            string input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine("No input, try again!");
                Console.WriteLine("Press Enter to try again...");
                break;
            }

            if (!int.TryParse(input.Trim(), out int move)) {
                Console.WriteLine("Infalid type of input, try again!");
                Console.WriteLine("Press Enter to try again...");
                Console.ReadLine();
                continue;
            }

            if (move < 1 || move > 9) {
                Console.WriteLine("Number must be in 1-9 range.");
                Console.WriteLine("Press Enter to try again...");
                Console.ReadLine();
                continue;
            }

            if (!board.MakeMove(move, players[current].Mark)) {
                Console.WriteLine("This field is used, choose other.");
                Console.WriteLine("Press Enter to try again...");
                Console.ReadLine();
                continue;
            }

            if (board.HasWinner(players[current].Mark)) {
                Console.Clear();
                board.Display();
                Console.WriteLine();
                Console.WriteLine("Congratulation! {0} ({1}) win", players[current].Name, players[current].Mark);
                break;
            }

            if (board.IsFull()) {
                Console.Clear();
                board.Display();
                Console.WriteLine();
                Console.WriteLine("Draw!");
                break;
            }

            current = 1 - current; // switch player
        }
        Console.WriteLine("Game over, type any key to continue.");
        Console.ReadLine();
    }
}
