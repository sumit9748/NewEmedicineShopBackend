
using Emedicine.DAL.Data;
using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;


namespace Emedicine.BAL.MedcineBased
{
    public class MedicineMain : IMedicineMain
    {
        private readonly IDataAccess _da;
        public MedicineMain(IDataAccess da) 
        {
            _da = da;
        }
        //Get all medicines
        public async Task<IEnumerable<Medicine>> GetAllMedicine()
        {
            return await _da.medicine.GetAllAsync();
        }

        
        //Get medicines by filtering with its type parameter.
        public async Task<IEnumerable<Medicine>> GetTypeOfMedicine(string type)
        {
            return await _da.medicine.GetAllListAsync(x => x.Type == type);
        }
        //Get medicine by its id
        public async Task<Medicine> GetMedicineById(int id)
        {
            return await _da.medicine.GetFirstOrDefaultAsync(x => x.Id == id);
        }
        //get Medicalshop by its id 
        public async Task<Medicalshop> GetMedicalshopById(int id)
        {
            return await _da.medicalShop.GetFirstOrDefaultAsync(x => x.ID == id);
        }
        //Add the medicine into database
        //Used MedicineVm to add both medicine and medicalshopitems
        public async Task<bool> AddMedicine(MedicineVm medicine)
        {
            if (medicine == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                //get all medicines 
                var medicines = await _da.medicine.GetAllAsync();
                if(medicines.Any(m=>m.Name == medicine.Name))
                {
                    return await Task.FromResult(false);
                }
                //add medicine into medicine table
                var med = new Medicine
                {
                    Name = medicine.Name,
                    Manufacturer = medicine.Manufacturer,
                    UnitPrice = medicine.UnitPrice,
                    Discount = medicine.Discount,
                    ExpDate = medicine.ExpDate,
                    ImgUrl = medicine.ImgUrl,
                    Status = medicine.Status,
                    Type = medicine.Type,

                };
                 _da.medicine.AddAsync(med);
                _da.save();
                //second:-Add items into medicalshopitems table.
                /*
                foreach(var id in medicine.MedicalShops)
                {
                    //var medicalshop=await _da.medicalShop.GetFirstOrDefaultAsync(c=>c.ID== id);
                    var mItem = new MedicalShopItem
                    {
                        MedicalShopId = id,
                        MedicineId = med.Id,
                        //Medicine=med,
                        //MedicalShop= medicalshop,
                        Quantity=10,
                    };
                    _da.medicalShopItem.AddAsync(mItem);
                    _da.save();
                }*/
                return await Task.FromResult(true);
            }

        }
        //Update a medicine.
        public void UpdateMedicine(Medicine medicine)
        {
            _da.medicine.UpdateExisting(medicine);
            _da.save();
        }
        //Delete a medicine.
        public void DeleteMedicine(Medicine medicine)
        {
            _da.medicine.Remove(medicine);
            _da.save();
        }
        //Get medicines present in a medicalshop
        public async Task<IEnumerable<Medicine>> GetMedicalShopItem(int id)
        {
            return await _da.medicine.GetMedicalShopItems(id);
        }
        public async Task<IEnumerable<Medicine>> GetAllMedicines()
        {
            var medicines = await _da.medicine.GetAllAsync();
            return medicines;
        }
        public async Task<IEnumerable<Medicine>> GetMedicineByType(string type)
        {
            var medicines=await _da.medicine.GetAllListAsync(m=>m.Type== type);

            return medicines;
        }
    }
}
