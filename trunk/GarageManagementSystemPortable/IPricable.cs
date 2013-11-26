namespace GarageManagementSystem
{
    using System;

    public interface IPricable
    {
        decimal Price { get; set; }

        decimal CalculateMargin();
    }
}