using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTableDataAccess
{
    public static class AzureStorageConnection
    {

        private static CloudStorageAccount _instance;


        public static CloudStorageAccount Instance()
        {
            // Uses lazy initialization.

            if (_instance == null)
            {
                _instance = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=eduaccount;AccountKey=1aum0Jx/fz/xENwYqz+j7JRTnYS5cIsUUdfZ1XvQ2R7NnoIaObJ7bg4KxInTt1IlvISRKOebtBSrroUEl43AZA==;EndpointSuffix=core.windows.net");
            }

            return _instance;
        }

    }
}
