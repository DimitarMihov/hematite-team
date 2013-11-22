namespace GarageManagementSystem
{
    using System;
    using System.Linq;

    public enum CarStatus
    {
        New,
        Accepted, // The car is accepted for service/maintenance
        Rejected, // The car is not approved for service/maintenance
        InProgress,
        Feedback, // Waiting client to approve something
        Completed, // The car service/maintenance is fully completed
    }
}