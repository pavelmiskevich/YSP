using System.ComponentModel.DataAnnotations;

namespace YSP.Core.Models.Base
{
    public abstract class EntityBase
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// для проверки параллелизма
        /// </summary>
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        public bool? IsActive { get; set; }
    }
}
