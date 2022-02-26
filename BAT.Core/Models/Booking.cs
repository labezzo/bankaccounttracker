namespace BAT.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Booking
    {
        public Guid BookingId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// The first set amount (eg. when a regular booking differs from it's normal value)
        /// </summary>
        public double InitialAmount { get; set; }
        public DateTime Date { get; set; }

        public Booking(double amount, string description, DateTime date)
        {
            Amount = amount;
            InitialAmount = amount;
            Description = description;
            Date = date;
            BookingId = Guid.NewGuid();
        }
    }
}
