using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTableDataAccess
{
    public static class AzureStorageConnection
    {

        private static CloudStorageAccount _instance;


        public static CloudStorageAccount Instance(string connectionString, string accountName)
        {
            // Uses lazy initialization.

            if (_instance == null)
            {
                _instance = CloudStorageAccount.Parse(connectionString);
            }

            return _instance;
        }

    }
}
