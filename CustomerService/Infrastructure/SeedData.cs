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

                    for (int i = 0; i < count; i++)
                    {
                        Customer customer = new Customer
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = GetRandomArabicName(count),
                            Email = $"customer{i + 1}@example.com",
                            Password = "password", // You may want to generate random passwords
                            PhoneNumber = GetRandomPhoneNumber(),
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            CustomerAddresses = new List<CustomerAddress>
                                {
                                    new CustomerAddress
                                    {
                                        Id = Guid.NewGuid().ToString(),
                                        CustomerId = "", // Fill in customer ID when added to database
                                        Address = "Address " + (i + 1),
                                        GeoLat = 0, // Fill in actual coordinates
                                        GeoLon = 0, // Fill in actual coordinates
                                        CreateTime = DateTime.Now,
                                        UpdateTime = DateTime.Now
                                    }
                                }
                        };

                        customers.Add(customer);
                    }
                    context.Customers.AddRangeAsync(customers);
                    context.SaveChanges();
                }
            }
        }
        static private string GetRandomArabicName(int count)
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


            Random rand = new Random();
            return names[count-1];
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