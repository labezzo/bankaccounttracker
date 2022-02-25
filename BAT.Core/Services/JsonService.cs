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
        private readonly IFileSystemService _fileSystemService;

        public JsonService() : this(new FileSystemService()) { }

        public JsonService(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        public bool AddAccount(string bank, string name, double balance)
        {
            bool success = false;

            if (string.IsNullOrWhiteSpace(bank) || string.IsNullOrWhiteSpace(name))
            {
                LogService.LogInfo("bank or name is empty");
                return success;
            }

            var accounts = ReadAccountsFromJson();
            var account = accounts.FirstOrDefault(acc => acc.Bank.Equals(bank) && acc.Name.Equals(name));
            if (account == null)
            {
                account = new Account(bank, name, balance);
                WriteAccountToJson(accounts, account);
            }
            else
            {
                var logMsg = "account alread exists. bank|name: " + bank + "|" + name;
                LogService.LogWarn(logMsg);
            }

            return success;
        }

        public bool AddBooking(Account account, double amount, System.DateTime date, string description)
        {
            bool success = false;

            if (amount == 0.00 || string.IsNullOrWhiteSpace(description) || account == null)
            {
                LogService.LogInfo("bank or name is empty");
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
                LogService.LogInfo("bank or name is empty");
                return account;
            }

            var accounts = ReadAccountsFromJson();
            if (accounts != null)
            {
                account = accounts.FirstOrDefault(acc => acc.Bank.Equals(bank) && acc.Name.Equals(name));
            }
            else
            {
                LogService.LogInfo("there are no accounts");
            }

            return account;
        }

        public List<Booking> GetAllBookingsByAccount(Account account)
        {
            var bookings = new List<Booking>();

            if (account == null)
            {
                LogService.LogInfo("account is null");
                return bookings;
            }

            bookings = ReadBookingsFromJsons(account);

            return bookings;
        }

        private List<Account> ReadAccountsFromJson()
        {
            var accounts = new List<Account>();

            var jsonFile = _fileSystemService.GetOrCreateFileInfo(Consts.JsonPathAccounts);
            if (jsonFile == null || !jsonFile.Exists)
            {
                LogService.LogInfo("jsonfile does not exist");
                return accounts;
            }

            var json = _fileSystemService.ReadFromFile(jsonFile.FullName);

            var accountsFromJson = JsonConvert.DeserializeObject<List<Account>>(json);
            if (accountsFromJson != null && accountsFromJson.Any())
            {
                accounts = accountsFromJson;
            }
            else
            {
                LogService.LogInfo("there are no accounts in json");
            }

            return accounts;
        }

        private List<Booking> ReadBookingsFromJsons(Account account)
        {
            var bookings = new List<Booking>();

            var accountDirectory = _fileSystemService.GetAccountDirectoryInfo(account.AccountId.GetDirectoryFormat());
            if (accountDirectory == null || !accountDirectory.Exists)
            {
                LogService.LogInfo("accountDirectory does not exist");
                return bookings;
            }

            var bookingJsonFiles = accountDirectory.GetFiles();
            if (bookingJsonFiles == null || !bookingJsonFiles.Any())
            {
                LogService.LogInfo("there are no booking json files");
                return bookings;
            }

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

            return bookings;
        }

        private void WriteAccountToJson(List<Account> accounts, Account account)
        {
            accounts.Add(account);
            var accountsJson = JsonConvert.SerializeObject(accounts);
            _fileSystemService.WriteToFile(Consts.JsonPathAccounts, accountsJson);
        }
    }
}
