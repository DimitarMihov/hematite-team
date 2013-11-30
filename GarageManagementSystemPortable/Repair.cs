namespace GarageManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Repair : IPricable
    {
        public string Caption { get; set; }
        public int Guarantee { get; set; }
        public List<Part> ExchangedParts { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }

        public Repair(string caption, int guarantee, List<Part> exchangedParts, DateTime date)
        {
            this.Caption = caption;
            this.Guarantee = guarantee;
            this.ExchangedParts = exchangedParts;
            this.Date = date;
            this.Price = CalculateMargin();
        }

        public decimal CalculateMargin()
        {
            decimal sum = 0.0m;
            foreach (var part in ExchangedParts)
            {
                sum += part.Price;
            }

            sum += 10; // 10 levs for work

            return sum;
        }

        public static string SaveRepairInformation(Repair repair)
        {
            StringBuilder builder = new StringBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            var repairProperties = assembly.GetType("GarageManagementSystem.Repair").GetProperties();

            foreach (var property in repairProperties)
            {
                if (property.Name == "ExchangedParts")
                {
                    dynamic partList = property.GetValue(repair, null);
                    builder.AppendLine("ExchangedParts");
                    builder.AppendLine(partList.Count.ToString());
                    foreach (var part in partList)
                    {
                        builder.Append(Part.SavePartInformation(part));
                    }
                }
                else
                {
                    builder.AppendLine(property.Name);
                    builder.AppendLine(property.GetValue(repair, null).ToString());
                }
            }

            return builder.ToString();
        }
    }
}
