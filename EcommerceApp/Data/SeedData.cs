using EcommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Data
{
    public class SeedData
    {
        public static async System.Threading.Tasks.Task Initialize(IServiceProvider serviceProvider)
        {
            EcommerceAppContext context = new EcommerceAppContext(serviceProvider.GetRequiredService<DbContextOptions<EcommerceAppContext>>());

            context.Database.EnsureDeleted();
            context.Database.Migrate();

            // Seed Products to database
            try
            {
                if (!context.Product.Any())
                {
                    if (!context.Product.Any())
                    {
                        Product iphoneOne = new Product
                        {
                            ProductName = "iPhone 14",
                            ProductDescription = "The latest iPhone with advanced features",
                            PriceInCAD = 1000,
                            AvailableQuantity = 10
                        };

                        Product iphoneTwo = new Product
                        {
                            ProductName = "iPhone SE",
                            ProductDescription = "A compact and budget-friendly iPhone",
                            PriceInCAD = 500,
                            AvailableQuantity = 8
                        };

                        Product iphoneThree = new Product
                        {
                            ProductName = "iPhone Pro Max",
                            ProductDescription = "Flagship iPhone with a powerful camera",
                            PriceInCAD = 1200,
                            AvailableQuantity = 6
                        };

                        Product samsungOne = new Product
                        {
                            ProductName = "Samsung Galaxy S21",
                            ProductDescription = "High-end Samsung Galaxy phone",
                            PriceInCAD = 900,
                            AvailableQuantity = 12
                        };

                        Product samsungTwo = new Product
                        {
                            ProductName = "Samsung Galaxy A52",
                            ProductDescription = "Mid-range Samsung Galaxy with great features",
                            PriceInCAD = 600,
                            AvailableQuantity = 15
                        };

                        Product samsungThree = new Product
                        {
                            ProductName = "Samsung Galaxy Note 20",
                            ProductDescription = "Galaxy Note series with a stylus",
                            PriceInCAD = 1100,
                            AvailableQuantity = 9
                        };

                        Product pixelOne = new Product
                        {
                            ProductName = "Google Pixel 6",
                            ProductDescription = "Google's latest Pixel phone",
                            PriceInCAD = 800,
                            AvailableQuantity = 11
                        };

                        Product pixelTwo = new Product
                        {
                            ProductName = "Google Pixel 5a",
                            ProductDescription = "Affordable Pixel phone with great camera",
                            PriceInCAD = 400,
                            AvailableQuantity = 14
                        };

                        Product pixelThree = new Product
                        {
                            ProductName = "Google Pixel 4 XL",
                            ProductDescription = "Previous flagship Pixel phone",
                            PriceInCAD = 700,
                            AvailableQuantity = 7
                        };

                        Product oneplusOne = new Product
                        {
                            ProductName = "OnePlus 9 Pro",
                            ProductDescription = "Flagship killer with high refresh rate display",
                            PriceInCAD = 900,
                            AvailableQuantity = 8
                        };

                        Product oneplusTwo = new Product
                        {
                            ProductName = "OnePlus Nord",
                            ProductDescription = "Mid-range OnePlus phone with 5G",
                            PriceInCAD = 600,
                            AvailableQuantity = 10
                        };

                        Product oneplusThree = new Product
                        {
                            ProductName = "OnePlus 8T",
                            ProductDescription = "OnePlus phone with fast charging",
                            PriceInCAD = 800,
                            AvailableQuantity = 7
                        };

                        Product xiaomiOne = new Product
                        {
                            ProductName = "Xiaomi Mi 11",
                            ProductDescription = "High-end Xiaomi phone with Snapdragon 888",
                            PriceInCAD = 800,
                            AvailableQuantity = 9
                        };

                        Product xiaomiTwo = new Product
                        {
                            ProductName = "Xiaomi Redmi Note 10",
                            ProductDescription = "Affordable Redmi phone with a big display",
                            PriceInCAD = 300,
                            AvailableQuantity = 15
                        };

                        Product xiaomiThree = new Product
                        {
                            ProductName = "Xiaomi Poco X3",
                            ProductDescription = "Poco series with a powerful processor",
                            PriceInCAD = 400,
                            AvailableQuantity = 11
                        };

                        Product huaweiOne = new Product
                        {
                            ProductName = "Huawei P40 Pro",
                            ProductDescription = "Flagship Huawei phone with Leica camera",
                            PriceInCAD = 1000,
                            AvailableQuantity = 6
                        };

                        Product huaweiTwo = new Product
                        {
                            ProductName = "Huawei Mate 30",
                            ProductDescription = "Mate series phone with a big battery",
                            PriceInCAD = 900,
                            AvailableQuantity = 7
                        };

                        Product huaweiThree = new Product
                        {
                            ProductName = "Huawei Nova 7",
                            ProductDescription = "Mid-range Huawei phone with 5G support",
                            PriceInCAD = 600,
                            AvailableQuantity = 12
                        };

                        Product samsungFour = new Product
                        {
                            ProductName = "Samsung Galaxy S22",
                            ProductDescription = "High-end Samsung Galaxy with advanced camera",
                            PriceInCAD = 1000,
                            AvailableQuantity = 9
                        };

                        Product samsungFive = new Product
                        {
                            ProductName = "Samsung Galaxy Z Fold 4",
                            ProductDescription = "Foldable Samsung Galaxy phone",
                            PriceInCAD = 1500,
                            AvailableQuantity = 5
                        };

                        Product samsungSix = new Product
                        {
                            ProductName = "Samsung Galaxy A72",
                            ProductDescription = "Mid-range Samsung Galaxy with a large display",
                            PriceInCAD = 700,
                            AvailableQuantity = 11
                        };

                        context.Product.AddRange(
                            iphoneOne, iphoneTwo, iphoneThree,
                            samsungOne, samsungTwo, samsungThree,
                            samsungFour, samsungFive, samsungSix,
                            pixelOne, pixelTwo, pixelThree,
                            oneplusOne, oneplusTwo, oneplusThree,
                            xiaomiOne, xiaomiTwo, xiaomiThree,
                            huaweiOne, huaweiTwo, huaweiThree);
                        context.SaveChanges();
                    }

                    if (!context.Country.Any())
                    {
                        List<Country> countries = new List<Country>
                        {
                                new Country
                                {
                                    CountryName = "United States",
                                    CoversionRate = 0.75m, // 1 CAD = 0.75 USD
                                    TaxRate = 0.07m
                                },
                                new Country
                                {
                                    CountryName = "Canada",
                                    CoversionRate = 1.00m, // 1 CAD = 1.00 CAD (Equivalent)
                                    TaxRate = 0.05m
                                },
                                new Country
                                {
                                    CountryName = "United Kingdom",
                                    CoversionRate = 1.20m, // 1 CAD = 1.20 GBP
                                    TaxRate = 0.20m
                                },
                                new Country
                                {
                                    CountryName = "Australia",
                                    CoversionRate = 0.70m, // 1 CAD = 0.70 AUD
                                    TaxRate = 0.10m
                                },
                                new Country
                                {
                                    CountryName = "Germany",
                                    CoversionRate = 0.85m, // 1 CAD = 0.85 EUR
                                    TaxRate = 0.19m
                                },
                                new Country
                                {
                                    CountryName = "Japan",
                                    CoversionRate = 0.0091m, // 1 CAD = 0.0091 JPY
                                    TaxRate = 0.08m
                                }
                        };


                        context.Country.AddRange(countries);
                        context.SaveChanges();
                    }
                }
                 
                if (!context.Carts.Any())
                {
                    Cart cart = new Cart();
                    context.Carts.Add(cart);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
