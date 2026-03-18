using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBattleLibrary.Models
{
    public class PlayerInfoModel
    {
        public string PlayerName { get; set; }
        public List<GridSpotModel> PlayerShipLocations { get; set; } = new List<GridSpotModel>();
        public List<GridSpotModel> PlayerShots { get; set; } = new List<GridSpotModel>();
    }
}
