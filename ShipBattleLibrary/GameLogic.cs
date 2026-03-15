using ShipBattleLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShipBattleLibrary
{
    public static class GameLogic
    {
        // This is here because it needs no UI
        public static void InitializeGrid(PlayerInfoModel player)
        {
            /**
             * This is supposed to be done in the Logic step
             */

            //List<string> letters = new List<string>
            //{
            //    "A",
            //    "B",
            //    "C",
            //    "D",
            //    "E"
            //};

            //List<int> numbers = new List<int>
            //{
            //    1,
            //    2,
            //    3,
            //    4,
            //    5
            //};

            //foreach (string letter in letters)
            //{
            //    foreach (int number in numbers)
            //    {
            //        AddGridSpot(player, letter, number);
            //    }
            //}
        }

        /**
         * This is supposed to be done in the Logic step
         */

        //private static void AddGridSpot(PlayerInfoModel player, string letter,
        //    int number)
        //{
        //    GridSpotModel spot = new GridSpotModel
        //    {
        //        SpotLetter = letter,
        //        SpotNumber = number,
        //        Status = GridSpotStatus.Empty
        //    };

        //    player.PlayerShots.Add(spot);
        //}


        public static bool PlaceShip(PlayerInfoModel player, string location)
        {
            throw new NotImplementedException();
        }
    }
}
