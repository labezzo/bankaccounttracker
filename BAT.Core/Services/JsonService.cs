namespace BAT.Core.Services
{
    using BAT.Core.Helpers;
    using BAT.Core.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class JsonService : IJsonService
    {
        public JsonService()
        {

        }

        public bool AddAccount(string bank, string name, double balance)
        {
            bool success = false;

            if (!string.IsNullOrWhiteSpace(bank) && !string.IsNullOrWhiteSpace(name))
            {
                var accounts = ReadAccountsFromJson();
                var account = accounts.FirstOrDefault(acc => acc.Bank.Equals(bank) && acc.Name.Equals(name));
                if (account == null)
                {
                    account = new Account(bank, name, balance);
                    WriteAccountToJson(accounts, account);
                }
                else
                {
                    // todo: logging -> account already exists
                }
            }

            return success;
        }

        public bool AddBooking(Account account, double amount, System.DateTime date, string description)
        {
            bool success = false;

            if (amount == 0.00 || string.IsNullOrWhiteSpace(description) || account == null)
            {
                return success;
            }

            // todo: implement
            throw new NotImplementedException();

            return success;
        }

        public Account GetAccount(string bank, string name)
        {
            Account account = null;

            if (string.IsNullOrWhiteSpace(bank) || string.IsNullOrWhiteSpace(name))
            {
                return account;
            }

            var accounts = ReadAccountsFromJson();
            if (accounts != null)
            {
                account = accounts.FirstOrDefault(acc => acc.Bank.Equals(bank) && acc.Name.Equals(name));
            }

            return account;
        }

        public List<Booking> GetAllBookingsByAccount(Account account)
        {
            var bookings = new List<Booking>();

            if (account == null)
            {
                return bookings;
            }

            bookings = ReadBookingsFromJsons(account);

            return bookings;
        }

        private List<Account> ReadAccountsFromJson()
        {
            var accounts = new List<Account>();

            var jsonFileExists = File.Exists(Consts.JsonPathAccounts);
            if (!jsonFileExists)
            {
                jsonFileExists = CreateFile(Consts.JsonPathAccounts);
            }

            if (jsonFileExists)
            {
                using (var streamReader = new StreamReader(Consts.JsonPathAccounts))
                {
                    var json = streamReader.ReadToEnd();
                    var accountsFromJson = JsonConvert.DeserializeObject<List<Account>>(json);
                    if (accountsFromJson != null && accountsFromJson.Any())
                    {
                        accounts = accountsFromJson;
                    }
                }
            }

            return accounts;
        }

        private List<Booking> ReadBookingsFromJsons(Account account)
        {
            var bookings = new List<Booking>();

            var bookingDirectory = GetOrCreateDirectory(Consts.PathBookings);

            if (bookingDirectory.Exists)
            {
                var accountDirectoryPath = Path.Combine(Consts.PathBookings, account.AccountId.GetDirectoryFormat());
                var accountDirectory = GetOrCreateDirectory(accountDirectoryPath);

                if (accountDirectory.Exists)
                {
                    var bookingJsonFiles = accountDirectory.GetFiles();
                    if (bookingJsonFiles != null && bookingJsonFiles.Any())
                    {
                        foreach (var bookingJsonFile in bookingJsonFiles)
                        {
                            using (var streamReader = new StreamReader(bookingJsonFile.FullName))
                            {
                                var json = streamReader.ReadToEnd();
                                var bookingsFromJson = JsonConvert.DeserializeObject<List<Booking>>(json);
                                if (bookingsFromJson != null && bookingsFromJson.Any())
                                {
                                    bookings.AddRange(bookingsFromJson);
                                }
                            }
                        }
                    }
                }
            }

            return bookings;
        }

        private void WriteAccountToJson(List<Account> accounts, Account account)
        {
            accounts.Add(account);
            var accountsJson = JsonConvert.SerializeObject(accounts);
            File.WriteAllText(Consts.JsonPathAccounts, accountsJson);
        }

        private bool CreateFile(string path)
        {
            File.Create(path).Close();
            var success = File.Exists(path);

            return success;
        }

        private DirectoryInfo GetOrCreateDirectory(string path)
        {
            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
            {
                directory = Directory.CreateDirectory(path);
            }

            return directory;
        }
    }
}
