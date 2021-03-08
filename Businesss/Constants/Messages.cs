using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class SystemMessages
    {
        public static string MaintenanceTime = "Sistem Bakımda";      
        public static string AuthorizationDenied = "Yetkiniz yok";

    }
    public static class ProductMessages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün İsmi Geçersiz";
        public static string ProductsListed = "Ürünler Listelendi";
        public static string ProductCountOfCategoryError = "Kategorideki Ürün Sınırına Ulaşıldı";
        public static string ProductNameAlreadyExists = "Bu İsimde Zaten Bir Ürün Var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı içiin yeni kategory eklenemiyor";
    }
    public static class AuthMessages
    {
        public static string Registered = "Kayıt olundu";
        public static string Login = "Giriş yapıldı";
        public static string UserAvailable = "Kullanıcı Mevcut";
        public static string TokenCreated = "Token oluşturuldu";
    }
    public static class ProductImageMessage
    {
        public static string ProductImageLimitExceeded = "En Fazla 5 Resim Ekleyebilirsiniz";
        public static string NoPicture = "Resim yok";
    }
}
