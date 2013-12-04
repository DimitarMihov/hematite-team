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

        public Repair() { }

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
                    try
                    {
                        builder.AppendLine(property.GetValue(repair, null).ToString());
                    }
                    catch (NullReferenceException)
                    {
                        builder.AppendLine("-");
                    }
                }
            }

            return builder.ToString();
        }

        public static Repair LoadRepairInformation(string[] lines, ref int index)
        {
            Repair repair = new Repair();
            var assembly = Assembly.GetExecutingAssembly();
            var userType = assembly.GetType("GarageManagementSystem.Repair");
            int propertiesCount = Service.PropertiesCount(repair);

            for (int i = 0; i < propertiesCount; i++, index++)
            {
                var property = userType.GetProperty(lines[index]);

                if (property.Name == "ExchangedParts")
                {
                    List<Part> parts = new List<Part>();

                    int stopPoint = int.Parse(lines[index + 1]);
                    index += 2;

                    for (int element = 0; element < stopPoint; element++)
                    {
                        parts.Add(Part.LoadPartInformation(lines, ref index));
                    }

                    index--;
                    property.SetValue(repair, parts, null);
                }
                else
                {
                    index++;

                    if (lines[index] != "-")
                    {
                        var currentPropertyType = property.PropertyType;
                        var convertedValue = Convert.ChangeType(lines[index], currentPropertyType, null);
                        property.SetValue(repair, convertedValue, null);
                    }
                }
            }

            return repair;
        }
    }
}
