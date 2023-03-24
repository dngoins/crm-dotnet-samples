using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;
using Microsoft.Xrm.Sdk;
//using CrmEarlyBound;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Metadata;
using System.Security.Principal;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Linq;


namespace CRMDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Dummy connection
            var connectionString = $@"Url=https://org3ba05cbf.crm.dynamics.com/;
                AuthType = OAuth;
                UserName = blah@blah.com;
                Password = TryThisPass1234;
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

                //Account newAct = new Account();
                //newAct.Name = "Avanade New Account 2";
                //svc.Create(newAct);

                   //3. Retrieve active contacts parented by accounts in Redmond
                FetchExpression fetch_query = new FetchExpression(
                    $@"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'> 
                        <entity name='contact'>
                            <attribute name='fullname' />
                            <attribute name='contactid' />
                            <attribute name='firstname' />
                            <attribute name='lastname' />
                            <order attribute='lastname' descending='false' />
                            <filter type='and'>
                                <condition attribute='statecode' operator='eq' value='0' />
                            </filter>
                            <link-entity name='account' from='accountid' to='parentcustomerid' alias='ae' >
                                <attribute name='name'/>
                                <filter type='and'>
                                    <condition attribute='address1_city' operator='eq' value='Redmond' />
                                </filter>
                            </link-entity>
                         </entity>
                       </fetch>");
                

                List < Entity > fect_contacts = svc.RetrieveMultiple(fetch_query).Entities.ToList();

            }

            Console.ReadLine();

        }
    }
}
