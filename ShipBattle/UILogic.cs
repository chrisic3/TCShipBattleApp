using ShipBattleLibrary;
using ShipBattleLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBattle
{
    public class UILogic
    {
        public static void DisplayWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations to {winner.PlayerName} for winning!");
            Console.WriteLine($"{winner.PlayerName} used {GameLogic.GetShotCount(winner)} shots.");
            Console.WriteLine("Thank you for playing Ship Battle.");
            Console.WriteLine("Press the \"Enter\" key to close.");
        }

        // Placed here and not in game logic because of Console specific needs
        public static void RecordPlayerShot(PlayerInfoModel currentPlayer, PlayerInfoModel opponent)
        {
            bool isValidShot = false;
            string row = string.Empty;
            int column = 0;

            do
            {
                string shot = AskForShot(currentPlayer);
                try
                {
                    (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                    isValidShot = GameLogic.ValidateShot(currentPlayer, row, column);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    isValidShot = false;
                }

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            } while (isValidShot == false);

            bool isAHit = GameLogic.IdentifyShotResults(opponent, row, column);

            GameLogic.MarkShotResult(currentPlayer, row, column, isAHit);

            DisplayShotResults(row, column, isAHit);
        }

        private static void DisplayShotResults(string row, int column, bool isAHit)
        {
            if (isAHit)
            {
                Console.WriteLine($"{row}{column} is a hit!\n");
            }
            else
            {
                Console.WriteLine($"{row}{column} is a miss.\n");
            }
        }

        // Placed here and not in game logic because of Console specific needs
        public static void DisplayShotGrid(PlayerInfoModel currentPlayer)
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
                // This is just here to indicate a bug if more statuses are added and we forget to adjust
                else
                {
                    Console.WriteLine(" ?? ");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Ship Battle, based on the game Battleship");
            Console.WriteLine("Designed by Tim Corey");
            Console.WriteLine("Built and modified by Chris Stelly\n");
        }

        public static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel();

            Console.WriteLine($"Lets set up {playerTitle}");

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

        private static string AskForShot(PlayerInfoModel player)
        {
            Console.Write($"{player.PlayerName}, please enter your shot: ");
            string output = Console.ReadLine();
            Console.WriteLine();

            return output;
        }

        // This is here becase it needs UI
        private static void PlaceShips(PlayerInfoModel player)
        {
            do
            {
                Console.Write($"Where do you want to place ship number {player.PlayerShipLocations.Count + 1} (Ex. D4): ");
                string location = Console.ReadLine();

                bool isValid = false;

                try
                {
                    isValid = GameLogic.PlaceShip(player, location);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                if (!isValid)
                {
                    Console.WriteLine("That was an invalid position. Please try again.");
                }
            } while (player.PlayerShipLocations.Count < 5); // May make dynamic
        }
    }
}
