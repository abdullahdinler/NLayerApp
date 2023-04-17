using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Exceptions
{
    #region Info

    // ClientSideException sınıfı Exception sınıfından türetilmiştir.
    // Burada gelen message parametresi Exception sınıfının message parametresine atanmıştır.
    // Bu sayede ClientSideException sınıfı ile bir hata oluştuğunda bu hata mesajını Exception sınıfının message parametresine atayarak 
    // Exception sınıfının message parametresini kullanarak hata mesajını döndürmüş olacağız. 
    // Bu sayede hata mesajını döndürürken Exception sınıfının message parametresini kullanacağız.


    #endregion
    public class ClientSideException : Exception
    {
        public ClientSideException(string message) : base(message)
        {
            
        }
    }
}
