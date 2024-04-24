using CustomerService.Domain;
using CustomerService.Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace Library.DataAccess.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MasterContext(
                serviceProvider.GetRequiredService<DbContextOptions<MasterContext>>()))
            {

                context.Database.Migrate();

                if (!context.Customers.Any())
                {

                    var count = 10;
                    List<Customer> customers = new List<Customer>(); 
                    var id = "691729e1-3d23-4af3-bdcc-b7545078d5e";
                    var addressId = "691729e1-3d23-4af3-bdcc-b75458rrr";
                    for (int i = 0; i < count; i++)
                    {
                        var custId = id + i;
                        Customer customer = new Customer
                        {
                            Id = custId,
                            Name = GetRandomArabicName(i),
                            Email = $"customer{i}@example.com",
                            Password = "password", // You may want to generate random passwords
                            PhoneNumber = GetRandomPhoneNumber(),
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            CustomerAddresses = new List<CustomerAddress>
                             {
                                 new CustomerAddress
                                 {
                                     Id =  addressId+i,
                                     CustomerId = custId, // Fill in customer ID when added to database
                                     Address = "Address " + i,
                                     GeoLat = 0, // Fill in actual coordinates
                                     GeoLon = 0, // Fill in actual coordinates
                                     CreateTime = DateTime.Now,
                                     UpdateTime = DateTime.Now
                                 }
                             }
                        };

                        customers.Add(customer);
                    }
                    try
                    {
                        Console.WriteLine(customers);
                        context.Customers.AddRange(customers);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
            }

        }
        static private string GetRandomArabicName(int index)
        {
            // You can add more names to the list according to your needs
            string[] names = {
                "علي النظاري",
                "محمد الحسني",
                "أحمد العوضي",
                "نور الدين",
                "سارة المصري",
                "فاطمة السعدي",
                "يوسف الجابري",
                "أمير العبدلي",
                "ريم الغامدي",
                "زينب الشمري"
            };


            return names[index];
        }
        static private string GetRandomPhoneNumber()
        {
            Random rand = new Random();
            int[] prefixes = { 77, 71, 73, 70 };
            int selectedPrefix = prefixes[rand.Next(0, prefixes.Length)];
            return $"+967 {selectedPrefix}{rand.Next(1000000, 10000000):0000000}";
        }


    }
}