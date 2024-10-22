using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3V13
{
    public class Item
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }


    public interface IInventory
    {
        int WarehouseId { get; set; }
        string WarehouseName { get; set; }
        int StorageCapacity { get; set; }

        void GetStorageStatus();
        void AddItem(Item item);
        void RemoveItem(Item item);
    }


    public class Inventory : IInventory
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int StorageCapacity { get; set; }

        public Inventory(int warehouseId, string warehouseName, int storageCapacity)
        {
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
            StorageCapacity = storageCapacity;
        }

        public virtual void GetStorageStatus()
        {
            Console.WriteLine($"Storage status:\n" +
                $"WarehouseId={WarehouseId}\n" +
                $"WarehouseName={WarehouseName}\n" +
                $"Capacity = {StorageCapacity}");
        }

        public virtual void AddItem(Item item)
        {
            StorageCapacity += 1;
            Console.WriteLine($"Adding item {item.Name} to the inventory...");
        }

        public virtual void RemoveItem(Item item)
        {
            StorageCapacity -= 1;
            Console.WriteLine($"Removing item {item.Name} to the inventory...");
        }
    }


    public class PersonalInventory : Inventory
    {
        public string OwnerName { get; set; }

        public PersonalInventory(
            int warehouseId, string warehouseName, int storageCapacity, string ownerName
            ) : base(warehouseId, warehouseName, storageCapacity)
        {
            OwnerName = ownerName;
        }

        public override void GetStorageStatus()
        {
            base.GetStorageStatus();
            Console.WriteLine($"Owner = {OwnerName}");
        }
    }


    public class GroupInventory : Inventory
    {
        public string ProductGroup { get; set; }

        public GroupInventory(
            int warehouseId, string warehouseName, int storageCapacity, string productGroup
            ) : base(warehouseId, warehouseName, storageCapacity)
        {
            ProductGroup = productGroup;
        }

        public override void AddItem(Item item)
        {
            base.AddItem(item);
            Console.WriteLine($"Product group: {ProductGroup}");
        }
    }


    public class AutomatedInventory : Inventory
    {
        public int AutomationLevel { get; set; }

        public AutomatedInventory(
            int warehouseId, string warehouseName, int storageCapacity, int automationLevel
            ) : base(warehouseId, warehouseName, storageCapacity)
        {
            AutomationLevel = automationLevel;
        }

        public override void RemoveItem(Item item)
        {
            base.RemoveItem(item);
            Console.WriteLine($"Automation level: {AutomationLevel}");
        }
    }


    public class InventoryCollection<T> where T : Inventory
    {
        private List<T> _inventories = new List<T>();

        public void Add(T item)
        {
            _inventories.Add(item);
        }

        public void Remove(T item)
        {
            _inventories.Remove(item);
        }

        public void DisplayInventories()
        {
            foreach (var inventory in _inventories)
            {
                inventory.GetStorageStatus();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            InventoryCollection<Inventory> inventories = new InventoryCollection<Inventory>();
            inventories.Add(new PersonalInventory(1, "Personal Warehouse", 100, "Лера"));
            inventories.Add(new GroupInventory(2, "Group Warehouse", 50, "Electronics"));
            inventories.Add(new AutomatedInventory(3, "Automated Warehouse", 200, 3));

            inventories.DisplayInventories();
            Console.ReadKey();
        }
    }
}
