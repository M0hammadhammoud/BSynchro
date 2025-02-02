﻿namespace BSynchro.RJP.Accounts.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
