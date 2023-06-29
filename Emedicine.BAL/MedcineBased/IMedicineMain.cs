using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;


namespace Emedicine.BAL.MedcineBased
{
    public interface IMedicineMain
    {
        
        public Task<IEnumerable<Medicine>> GetAllMedicine();
        public Task<IEnumerable<Medicine>> GetTypeOfMedicine(string type);
        public Task<Medicine> GetMedicineById(int id);
        public Task<Medicalshop> GetMedicalshopById(int id);
        public Task<bool> AddMedicine(MedicineVm medicine);
        public void UpdateMedicine(Medicine medicine);
        public void DeleteMedicine(Medicine medicine);
        public Task<IEnumerable<Medicine>> GetMedicalShopItem(int id);
        public Task<IEnumerable<Medicine>> GetAllMedicines();
        public Task<IEnumerable<Medicine>> GetMedicineByType(string type);
    }
}
