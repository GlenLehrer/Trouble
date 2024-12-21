using System.Xml;

namespace ZGT.Trouble.BL.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int NumberOfWins { get; set; }
        public DateTime DateJoined { get; set; }

    }
}
