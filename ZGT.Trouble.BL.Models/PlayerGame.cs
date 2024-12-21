using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZGT.Trouble.BL.Models
{
    public class PlayerGame
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid GameId { get; set; }
        public string PlayerColor { get; set; }
        public bool IsComputerPlaying { get; set; } = false;

    }
}
