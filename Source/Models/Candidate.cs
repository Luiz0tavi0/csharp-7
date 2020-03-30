using Codenation.Challenge.Models;
using System;

//Ajustei namespaces para ficar igual do teste
namespace Codenation.Challenge.Models
{
    public class Candidate
    {
        public int UserId { get; set; } //Chave estrangeira
        public User User { get; set; } //Propriedade de navegação

        public int AccelerationId { get; set; } //Chave estrangeira
        public Acceleration Acceleration { get; set; } //Propriedade de navegação

        public int CompanyId { get; set; } //Chave estrangeira
        public Company Company { get; set; } //Propriedade de navegação

        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
