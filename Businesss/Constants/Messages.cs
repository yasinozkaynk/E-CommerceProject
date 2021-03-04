using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOfCategoryError = "kategorideki ürün sınırına ulaşıldı";
        public static string ProductNameAlreadyExists = "Bu isimde zaten bir ürün var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı içiin yeni kategory eklenemiyor";
        public static string AuthorizationDenied = "yetkiniz yok";
    }
}
