using AzureTableDataAccess;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=eduaccount;AccountKey=1aum0Jx/fz/xENwYqz+j7JRTnYS5cIsUUdfZ1XvQ2R7NnoIaObJ7bg4KxInTt1IlvISRKOebtBSrroUEl43AZA==;EndpointSuffix=core.windows.net";
            var tableName = "Projects";

            CloudStorageAccount storageAccount;
            storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            CloudTable table = tableClient.GetTableReference(tableName);
            var project1 = new ProjectEntity("sec", "seeec");
            TableOperation inserOrMergeOpeartion = TableOperation.InsertOrMerge(project1);
            table.Execute(inserOrMergeOpeartion);
            Console.WriteLine("Success");
            Console.Read();


            
        }

        public class ProjectEntity : TableEntity
        {
            public object additionalField { get; set; }
            public ProjectEntity() { }
            public ProjectEntity(string fname, string lname)
            {
                PartitionKey = fname;
                RowKey = lname;

            }
        }
    }
}
