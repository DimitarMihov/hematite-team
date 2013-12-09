using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
    public struct StructNavigator
    {
        private int vehicleIndex;
        private int repairIndex;
        private int partIndex;
        private int vehicleInfromationIndex;
        private int distributorIndex;
        private bool isDistributor;

        public int VehicleIndex
        {
            get { return vehicleIndex; }
            set { vehicleIndex = value; }
        }
        public int RepairIndex
        {
            get { return repairIndex; }
            set { repairIndex = value; }
        }
        public int PartIndex
        {
            get { return partIndex; }
            set { partIndex = value; }
        }
        public int VehicleInfromationIndex
        {
            get { return vehicleInfromationIndex; }
            set { vehicleInfromationIndex = value; }
        }

        public int DistributorIndex
        {
            get { return distributorIndex; }
            set { distributorIndex = value; }
        }
        public bool IsDistributor
        {
            get { return isDistributor; }
            set { isDistributor = value; }
        }

        public StructNavigator(int vehicleIndex, int repairIndex, int partIndex, int vehicleInformationIndex)
        {
            this.vehicleIndex = vehicleIndex;
            this.repairIndex = repairIndex;
            this.partIndex = partIndex;
            this.vehicleInfromationIndex = vehicleInformationIndex;
            this.distributorIndex = -1;
            this.isDistributor = false;
        }
    }
}
