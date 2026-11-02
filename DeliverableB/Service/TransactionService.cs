using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Service
{
    public class TransactionService
    {
        private readonly AppDbContext _db;

        public TransactionService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ItemTransaction>> GetAllTransactionsAsync()
        {
            return await _db.ItemTransactions
                .Include(x => x.Item)
                .Include(x => x.Supplier)
                .Include(x => x.Staff)
                .ToListAsync();
        }

        public async Task<ItemTransaction?> GetTransactionAsync(int transactionId)
        {
            return await _db.ItemTransactions
                .Include(x => x.Item)
                .Include(x => x.Supplier)
                .Include(x => x.Staff)
                .FirstOrDefaultAsync(x => x.Transaction_ID == transactionId);
        }

        public async Task<(bool Success, string Message)> AddTransactionAsync(ItemTransaction transaction)
        {
            using var dbTransaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var item = await _db.Items.FirstOrDefaultAsync(x => x.Item_ID == transaction.Item_ID);

                if (item == null)
                {
                    return (false, "Item not found.");
                }

                if (transaction.Qty <= 0)
                {
                    return (false, "Quantity must be greater than zero.");
                }

                if (string.IsNullOrWhiteSpace(transaction.Transaction_Type))
                {
                    return (false, "Transaction type is required.");
                }

                if (transaction.Transaction_Type != "R" && transaction.Transaction_Type != "I")
                {
                    return (false, "Transaction type must be R or I.");
                }

                if (transaction.Transaction_Type == "R")
                {
                    if (transaction.Supplier_ID == null)
                    {
                        return (false, "Supplier is required for receive transactions.");
                    }

                    transaction.Staff_ID = null;
                    item.Qty_On_Hand += transaction.Qty;
                }

                if (transaction.Transaction_Type == "I")
                {
                    if (transaction.Staff_ID == null)
                    {
                        return (false, "Staff is required for issue transactions.");
                    }

                    if (item.Qty_On_Hand < transaction.Qty)
                    {
                        return (false, "Not enough stock available.");
                    }

                    transaction.Supplier_ID = null;
                    item.Qty_On_Hand -= transaction.Qty;
                }

                if (transaction.Transaction_Date == default)
                {
                    transaction.Transaction_Date = DateTime.Now;
                }

                _db.ItemTransactions.Add(transaction);

                await _db.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                return (true, "Transaction saved successfully.");
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                return (false, ex.Message);
            }
        }

        public async Task<List<ItemTransaction>> GetTransactionsByItemAsync(int itemId)
        {
            return await _db.ItemTransactions
                .Include(x => x.Item)
                .Include(x => x.Supplier)
                .Include(x => x.Staff)
                .Where(x => x.Item_ID == itemId)
                .ToListAsync();
        }
    }
}