using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Service
{
    public class SupplierService
    {
        private readonly AppDbContext _db;

        public SupplierService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return await _db.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetSupplierAsync(int supplierId)
        {
            return await _db.Suppliers.FirstOrDefaultAsync(x => x.Supplier_ID == supplierId);
        }

        public async Task<(bool Success, string Message)> AddSupplierAsync(Supplier supplier)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(supplier.Supplier_Name))
                {
                    return (false, "Supplier name is required.");
                }

                _db.Suppliers.Add(supplier);
                await _db.SaveChangesAsync();

                return (true, "Supplier added successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> UpdateSupplierAsync(Supplier supplier)
        {
            try
            {
                var existingSupplier = await _db.Suppliers.FirstOrDefaultAsync(x => x.Supplier_ID == supplier.Supplier_ID);

                if (existingSupplier == null)
                {
                    return (false, "Supplier not found.");
                }

                if (string.IsNullOrWhiteSpace(supplier.Supplier_Name))
                {
                    return (false, "Supplier name is required.");
                }

                existingSupplier.Supplier_Name = supplier.Supplier_Name;

                await _db.SaveChangesAsync();

                return (true, "Supplier updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> DeleteSupplierAsync(int supplierId)
        {
            try
            {
                var supplier = await _db.Suppliers.FirstOrDefaultAsync(x => x.Supplier_ID == supplierId);

                if (supplier == null)
                {
                    return (false, "Supplier not found.");
                }

                _db.Suppliers.Remove(supplier);
                await _db.SaveChangesAsync();

                return (true, "Supplier deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}