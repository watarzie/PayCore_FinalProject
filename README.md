# Patika.dev PayCore Bootcamp Bitirme Projesi
***
## Proje Tanımı
   Proje 195-PayCore .Net/Net.Core Bootcamp süreci içerisinde verilen bitirme projesidir. Projede ASP.NET WEB API ile bir uygulama yapılması istenmiştir.
   Login ve Register işlemleri yapılmış,JWT token ile authurization sağlanmıştır. Üretilen key API'nin header kısmına girilerek sisteme authorize olunabilir.
   Product Offer Category ve User tablolarından oluşan bir entity yapısı vardır.Bağlantılar Nhibernate mapping ile sağlanmıştır.Tablolar arası ilişkiler sağlanmıştır.
   HangFire ile bir E-mail servisi kodlanmış login ve register işlemlerinde kullanıcıya smtp ile mail yollanması sağlanmıştır.XUnit test ile mail testi yazılmıştır
***
## Kullanılan Teknolojiler
* ASP.NET Core WEB API
* .Net 5.0
* Hangfire
* Smtp
* AutoMapper
* Nibernate
* PostgreSql
***
## Proje Kullanımı 
* Projeyi klonlayın
```
 git clone https://github.com/watarzie/PayCore_FinalProject.git
```
* DataBaseScript dosyası ile postgresql kurulumunu yapınız.DataBase'in set olması yeterli.Proje bir kere build olunca tablolar otomatik oluşturulucaktır.

* localhost üzerinden hangfire dashboardına erişip kuyrukları ve mail servis yapısın görebilirsiniz. Bunun için url kısmına localhost ve port ibaresinden sonra hangfire sözcüğünü ekleyebilirsiniz

## Proje Ekran Görüntüleri

   






