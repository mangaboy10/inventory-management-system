using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Service
{
    public class ItemService
    {
        private readonly AppDbContext _db;

        public ItemService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _db.Items.ToListAsync();
        }

        public async Task<Item?> GetItemAsync(int itemId)
        {
            return await _db.Items.FirstOrDefaultAsync(x => x.Item_ID == itemId);
        }

        public async Task<(bool Success, string Message)> AddItemAsync(Item item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.Item_Name))
                {
                    return (false, "Item name is required.");
                }

                if (string.IsNullOrWhiteSpace(item.Unit))
                {
                    return (false, "Unit is required.");
                }

                if (item.Qty_On_Hand < 0)
                {
                    return (false, "Quantity on hand cannot be negative.");
                }

                if (item.Reorder_Level < 0)
                {
                    return (false, "Reorder level cannot be negative.");
                }

                _db.Items.Add(item);
                await _db.SaveChangesAsync();

                return (true, "Item added successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> UpdateItemAsync(Item item)
        {
            try
            {
                var existingItem = await _db.Items.FirstOrDefaultAsync(x => x.Item_ID == item.Item_ID);

                if (existingItem == null)
                {
                    return (false, "Item not found.");
                }

                if (string.IsNullOrWhiteSpace(item.Item_Name))
                {
                    return (false, "Item name is required.");
                }

                if (string.IsNullOrWhiteSpace(item.Unit))
                {
                    return (false, "Unit is required.");
                }

                if (item.Qty_On_Hand < 0)
                {
                    return (false, "Quantity on hand cannot be negative.");
                }

                if (item.Reorder_Level < 0)
                {
                    return (false, "Reorder level cannot be negative.");
                }

                existingItem.Item_Name = item.Item_Name;
                existingItem.Unit = item.Unit;
                existingItem.Qty_On_Hand = item.Qty_On_Hand;
                existingItem.Reorder_Level = item.Reorder_Level;

                await _db.SaveChangesAsync();

                return (true, "Item updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> DeleteItemAsync(int itemId)
        {
            try
            {
                var item = await _db.Items.FirstOrDefaultAsync(x => x.Item_ID == itemId);

                if (item == null)
                {
                    return (false, "Item not found.");
                }

                _db.Items.Remove(item);
                await _db.SaveChangesAsync();

                return (true, "Item deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<List<Item>> GetLowStockItemsAsync()
        {
            return await _db.Items
                .Where(x => x.Qty_On_Hand <= x.Reorder_Level)
                .ToListAsync();
        }
    }
}