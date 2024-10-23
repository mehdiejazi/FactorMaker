using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace FactorMaker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
                CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

/* TODO:
 * 
 * Store Dashboard:
 * *********************************
 * Done! Card : Factor Count By Store (This Year, This Mounth, This Week, This Day) 
 * Done! Card : Factor Sum of price By Store (This Year, This Mounth, This Week, This Day)
 *
 * Done! Chart: Factor Count by date by Store (Year to Year, Mounth to Mounth, Week To Week)
 * Done! Chart: Factor Sum of price by date by Store (Year to Year, Mounth to Mounth, Week To Week)
 * 
 * Done! Chart: Factor Count by time of day.
 * Done! Chart: Factor Sum of price by time of day.
 *
 * Done! Chart: Factor Count by day of Week
 * Done! Chart: Factor Sum of price by day of Week
 *
 * Done! Table: Product top 10 Sold Count By Store.
 * Done! Table: Product top 10 Sold Sum of price By Store.
 * 
 * Done! Chart: Customer top 10 Factor Count
 * Done! Chart: Customer top 10 Factor Sum of price
 * 
 * Chart: Customer By Gender
 * 
 * Done! Pie Chart: Product Sold Count By Store.
 * Done! Pie Chart: Product Sold Sum of price By Store.
 * 
 * Pie Chart: Category Sold Count By Store.
 * Pie Chart: Category Sold Sum of price By Store.
 * 
 * 
 */


