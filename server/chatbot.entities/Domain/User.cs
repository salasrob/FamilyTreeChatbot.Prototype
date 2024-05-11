
using System.Text.Json.Serialization;

namespace chatbot.entities.Domain
{
    public class User : UserBase
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsAccountActive { get; set; }
        public int CreatedBy { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public long ledger_start_transaction_id { get; set; }
        public long ledger_end_transaction_id { get; set; }
        public long ledger_start_sequence_number { get; set; }
        public long ledger_end_sequence_number { get; set; }
    }
}
