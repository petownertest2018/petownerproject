using PetOwnerApiClient.Interfaces;
using PetOwnerApiClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetOwnerApiClient.Factory;
using Autofac;
using PetOwnerApiClient.BusinessLogic;
using ApiClient = PetOwnerApiClient.Client.PetOwnerApiClient;
using PetOwnerApiClient.Service;
using System.Net.Http;
using System.Configuration;

namespace PetOwnerApiClientConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = GetBuilder();
            using (var container = builder.Build())
            {
                IPetOwnerApiClientFactory factory = container.Resolve<IPetOwnerApiClientFactory>();
                IPetOwnerApiClient client = container.Resolve<IPetOwnerApiClient>();
                factory.RegisterApiClient(ApiClientType.Default, client);

                var defaultClient = factory.GetApiClient(ApiClientType.Default);
                var petsByGenderResult = defaultClient.GetPetsByPetOwnerGender().Result;
                if (!string.IsNullOrEmpty(petsByGenderResult.Error))
                {
                    Console.WriteLine("There were errors while consuming the API.");
                    Console.WriteLine(petsByGenderResult.Error);
                }
                else
                {
                    foreach (var group in petsByGenderResult.PetsByGender)
                    {
                        Console.WriteLine("Gender: " + group.Gender);
                        Console.WriteLine("----------------------");
                        foreach (var name in group.PetNames)
                        {
                            Console.WriteLine(name);
                        }
                        Console.WriteLine("");
                    }
                }                
            }
            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }

        static ContainerBuilder GetBuilder()
        {
            var path = ConfigurationManager.AppSettings["PetOwnerAPI.Url"];
            var uri = new Uri(path);

            var builder = new ContainerBuilder();
            builder.Register(c => new PetOwnerApiService(new HttpClient(),uri)).As<IPetOwnerApiService>();
            builder.Register(c => new CatFinder()).As<IPetFinder>();
            builder.Register(c => new PetOwnerFinder()).As<IPetOwnerFinder>();         
            builder.RegisterType<JsonToPetOwnerConverter>().As<IJsonToPetOwnerConverter>();
            builder.Register(c => new AlphabeticComparer()).As<IComparer<string>>();
            builder.RegisterType<PetNameAlphabeticalSorter>().As<IPetSorter>();               
            builder.RegisterType<PetOwnerFilter>().As<IPetOwnerFilter>();              
            builder.Register(c => new PetOwnerGrouper()).As<IPetOwnerGrouper>();
            builder.RegisterType<ApiClient>().As<IPetOwnerApiClient>();
            builder.RegisterType<PetOwnerApiClientFactory>().As<IPetOwnerApiClientFactory>();
            return builder;
        }
    }
}
