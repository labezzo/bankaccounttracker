namespace BAT.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Account
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public string Bank { get; set; }
        public double Balance { get; set; }
        public List<Booking> Bookings { get; set; }

        public Account() : this(string.Empty, string.Empty, 0) { }

        public Account(string bank, string name, double balance)
        {
            Bank = bank;
            Name = name;
            Balance = balance;
            AccountId = Guid.NewGuid();
        }
    }
}
