using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün başarıyla eklendi";
        public static string ProductDeleted = "Ürün başarıyla silindi";
        public static string ProductUpdated = "Ürün başarıyla güncellendi";

        public static string CategoryAdded = "categori başarıyla eklendi";
        public static string CategoryDeleted = "categori başarıyla silindi";
        public static string CategoryUpdated = "categori başarıyla güncellendi";

        public static string UserNotFound = "kullanıcı bulunmadı";

        public static string PasswordError = "şifre hatalı";

        public static string SuccesssfulLogin = "sisteme giriş başarılı";

        public static string UserAlreadyExists = "bu kullanıcı zaten var";

        public static string UserRegistered = "kayıt olundu";

        public static string AccessTokenCreated = "access token başarıyla olşturuldu";

        public static string AuthorizationDenid = "Yetkiniz yok";

        public static string ProductNameAlreadyExists = "Ürün ismi zaten mevcut";
    }
}
