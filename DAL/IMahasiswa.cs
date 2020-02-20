using System.Collections.Generic;
using WorkshopASP.Models;

namespace WorkshopASP.DAL
{
    public interface IMahasiswa{
        IEnumerable<Mahasiswa> GetAll();
        Mahasiswa GetById(string id);
        void Insert(Mahasiswa mhs);
        void Update(Mahasiswa mhs);
        void Delete(string id);
    }
    
        
    
}