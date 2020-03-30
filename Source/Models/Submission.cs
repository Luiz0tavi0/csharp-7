using System;
namespace Source.Models
{
    public class Submission
    {
        public int UserId { get; set; } //Chave estrangeira
        public User User { get; set; } //Propriedade de navegação

        public int ChallengeId { get; set; } //Chave estrangeira
        public Challenge Challenge { get; set; } //Propriedade de navegação

        public decimal Score { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
