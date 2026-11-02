using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Service
{
    public class StaffService
    {
        private readonly AppDbContext _db;

        public StaffService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Staff>> GetAllStaffAsync()
        {
            return await _db.Staffs.ToListAsync();
        }

        public async Task<Staff?> GetStaffAsync(int staffId)
        {
            return await _db.Staffs.FirstOrDefaultAsync(x => x.Staff_ID == staffId);
        }

        public async Task<(bool Success, string Message)> AddStaffAsync(Staff staff)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(staff.Staff_Name))
                {
                    return (false, "Staff name is required.");
                }

                if (string.IsNullOrWhiteSpace(staff.Department))
                {
                    return (false, "Department is required.");
                }

                _db.Staffs.Add(staff);
                await _db.SaveChangesAsync();

                return (true, "Staff added successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> UpdateStaffAsync(Staff staff)
        {
            try
            {
                var existingStaff = await _db.Staffs.FirstOrDefaultAsync(x => x.Staff_ID == staff.Staff_ID);

                if (existingStaff == null)
                {
                    return (false, "Staff not found.");
                }

                if (string.IsNullOrWhiteSpace(staff.Staff_Name))
                {
                    return (false, "Staff name is required.");
                }

                if (string.IsNullOrWhiteSpace(staff.Department))
                {
                    return (false, "Department is required.");
                }

                existingStaff.Staff_Name = staff.Staff_Name;
                existingStaff.Department = staff.Department;

                await _db.SaveChangesAsync();

                return (true, "Staff updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> DeleteStaffAsync(int staffId)
        {
            try
            {
                var staff = await _db.Staffs.FirstOrDefaultAsync(x => x.Staff_ID == staffId);

                if (staff == null)
                {
                    return (false, "Staff not found.");
                }

                _db.Staffs.Remove(staff);
                await _db.SaveChangesAsync();

                return (true, "Staff deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}