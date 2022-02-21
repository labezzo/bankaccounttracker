namespace BAT.Core.Services
{
    using BAT.Core.Models;
    using System;
    using System.Collections.Generic;

    public interface IJsonService
    {
        bool AddAccount(string bank, string name, double balance);
        bool AddBooking(Account account, double amount, DateTime date, string description);
        Account GetAccount(string bank, string name);
        List<Booking> GetAllBookingsByAccount(Account account);
    }
}
