using System;
namespace Demo.Model {
    public class ModelBase {
        public long Id { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}