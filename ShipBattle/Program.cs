using ShipBattleLibrary;
using ShipBattleLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            PlayerInfoModel player1 = CreatePlayer("Player 1");
            PlayerInfoModel player2 = CreatePlayer("Player 2");

            PlayerInfoModel winner = null;

            do
            {
                PlayerInfoModel currentPlayer = player1;
                PlayerInfoModel opponent = player2;

                DisplayShotGrid(currentPlayer);

                RecordPlayerShot(currentPlayer, opponent);

                bool isGameOver = GameLogic.GameOver(opponent);

                if (isGameOver)
                {
                    winner = currentPlayer;
                }
                else
                {
                    // Neat trick using Tuples to swap players
                    (currentPlayer, opponent) = (opponent, currentPlayer);
                }
            } while (winner == null);

            DisplayWinner(winner);

            Console.ReadLine();
        }

        private static void DisplayWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations to {winner.PlayerName} " +
                $"for winning!");
            Console.WriteLine($"{winner.PlayerName} used " +
                $"{GameLogic.GetShotCount(winner)} shots.");
            Console.WriteLine("Thank you for playing Ship Battle.");
            Console.WriteLine("Press the \"Enter\" key to close.");
        }

        // Placed here and not in logic because of Console specific needs
        private static void RecordPlayerShot(PlayerInfoModel currentPlayer, 
            PlayerInfoModel opponent)
        {
            bool isValidShot = false;
            string row = string.Empty;
            int column = 0;

            do
            {
                string shot = AskForShot();
                (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                isValidShot = GameLogic.ValidateShot(currentPlayer, row, column);

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            } while (isValidShot == false);

            bool isAHit = GameLogic.IdentifyShotResults(opponent, row, column);

            GameLogic.MarkShotResult(currentPlayer, row, column, isAHit);
        }

        private static string AskForShot()
        {
            Console.Write("Please enter your shot: ");
            string output = Console.ReadLine();

            return output;
        }

        // Placed here and not in logic because of Console specific needs
        private static void DisplayShotGrid(PlayerInfoModel currentPlayer)
        {
            // This will store A, B, etc.
            string currentRow = currentPlayer.PlayerShots[0].SpotLetter;

            foreach (GridSpotModel gridSpot in currentPlayer.PlayerShots)
            {
                // Means row went from A to B etc.
                if (gridSpot.SpotLetter != currentRow) 
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }

                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
                }
                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X  ");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O  ");
                }
                // This is just here to indicate a bug if more statuses are
                // added and we forget to adjust
                else
                {
                    Console.WriteLine(" ?? ");
                }
            }
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Ship Battle, based on the game " +
                "Battleship");
            Console.WriteLine("Designed by Tim Corey");
            Console.WriteLine("Built and modified by Chris Stelly\n");
        }

        private static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel();

            Console.WriteLine($"Lets set up {playerTitle}.");

            output.PlayerName = AskForPlayerName();

            GameLogic.InitializeGrid(output);

            PlaceShips(output);

            Console.Clear();

            return output;
        }

        private static string AskForPlayerName()
        {
            Console.Write("What is your name: ");
            string output = Console.ReadLine();

            return output;
        }

        // This is here becase it needs UI
        private static void PlaceShips(PlayerInfoModel player)
        {
            do
            {
                Console.Write($"Where do you want to place ship number " +
                    $"{player.PlayerShipLocations.Count + 1} (Ex. D4): ");
                string location = Console.ReadLine();

                bool isValid = GameLogic.PlaceShip(player, location);

                if (!isValid)
                {
                    Console.WriteLine("That was an invalid position. Please " +
                        "try again.");
                }
            } while (player.PlayerShipLocations.Count < 5); // May make dynamic
        }
    }
}
