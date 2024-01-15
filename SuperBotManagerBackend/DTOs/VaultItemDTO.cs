using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.DB;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperBotManagerBackend.DTOs
{
    public class VaultItemUpdateDTO 
    {
        public int Id { get; set; }
        public string VaultGroupName { get; set; }
        public string FieldName { get; set; }
        public int OwnerId { get; set; }
        public Guid? SecretId { get; set; }
        public string? PlainValue { get; set; }
    }
    public class VaultItemDTO : VaultItemUpdateDTO
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

}
