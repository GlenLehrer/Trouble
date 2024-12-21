using ZGT.Trouble.PL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZGT.Trouble.PL.Entities
{
    public class tblPlayerGame : IEntity
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid GameId { get; set; }
        public string PlayerColor { get; set; }
        public bool IsComputerPlaying { get; set; } = false;
        public virtual tblPlayer Player { get; set; }//Used for Foreign keys
        public virtual tblGame Game { get; set; }//Used for Foreign keys



    }
}
