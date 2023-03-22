using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;
using Microsoft.Xrm.Sdk;
using CrmEarlyBound;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;

namespace CRMDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = $@"Url=https://org3ba05cbf.crm.dynamics.com/;
                AuthType = OAuth;
                UserName = dwight.goins@langanenterprises.com;
                Password = J@manaH999;
                AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
                RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
               
";

            using (CrmServiceClient svc = new CrmServiceClient(connectionString))
            {
                // use early binding
                var newAccount = new Entity("account");
                
                newAccount["name"] = "Avanade Account 1";

                var primaryKeyOfAccount = svc.Create(newAccount);

                Console.WriteLine( "New Account Created is: {0}", primaryKeyOfAccount.ToString());

                Account newAct = new Account();
                newAct.Name = "Avanade New Account 2";
                svc.Create(newAct);

                // Now let's use Query Expressions
                QueryExpression query = new QueryExpression("contact");
                query.ColumnSet = new ColumnSet("fullname");
                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, "Active");

                LinkEntity linkEntity = new LinkEntity("contact", "account", "parentcustomerid", "accountid", JoinOperator.Inner);
                linkEntity.LinkCriteria.AddCondition("address1_city", ConditionOperator.Equal, "Redmond");
                query.LinkEntities.Add(linkEntity);
                                
                List<Entity> contacts = svc.RetrieveMultiple(query).Entities.ToList();

            }

            Console.ReadLine();

        }
    }
}
