using ShipBattleLibrary;
using ShipBattleLibrary.Models;
using System;
using System.Collections.Generic;
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

                // Display shot grid for current player
                DisplayShotGrid(currentPlayer);

                // Ask current player for a shot
                // Determine if it is a valid shot
                // Determine shot results

                // Determine if the game is over

                // If over, set winner to current player

                // else, swap current player
                if (currentPlayer == player1)
                {
                    currentPlayer = player2;
                }
                else
                {
                    currentPlayer = player1;
                }
            } while (winner == null);

            Console.ReadLine();
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

            // Ask for player name
            output.PlayerName = AskForPlayerName();

            // Load the shot grid
            GameLogic.InitializeGrid(output);

            // Ask for ship locations
            PlaceShips(output);

            // Clear
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
