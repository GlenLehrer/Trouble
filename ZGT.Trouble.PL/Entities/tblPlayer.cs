using ZGT.Trouble.PL.Entities;
using System.Xml;

namespace ZGT.Trouble.PL.Entities
{
    public class tblPlayer : IEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int NumberOfWins { get; set; }
        public DateTime DateJoined { get; set; }
        public virtual List<tblPlayerGame> PlayerGame { get; set; }//Used for Foreign keys

    }
}
