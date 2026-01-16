# RealEstate CRM

Modern, ölçeklenebilir ve kullanıcı dostu bir Gayrimenkul CRM (Customer Relationship Management) uygulaması. Proje; emlak ofisleri ve bireysel danışmanların müşteri, ilan (property) ve lead süreçlerini tek bir panelden profesyonel şekilde yönetebilmesi için geliştirilmiştir.

(A modern, scalable, and user-friendly Real Estate CRM (Customer Relationship Management) application. The project was developed to enable real estate agencies and individual consultants to professionally manage client, property, and lead processes from a single dashboard.)



## Özellikler


### Kimlik Doğrulama & Yetkilendirme

- JWT tabanlı authentication
- Login / Logout yönetimi
- Toolbar üzerinde aktif kullanıcı bilgisi
- Yetkisiz erişimlere karşı koruma (Guard)

### Müşteri Yönetimi (Customers)

- Müşteri ekleme, düzenleme, silme
- Durum bazlı müşteri yönetimi
- Form validasyonları
- Responsive form tasarımları


### Lead Yönetimi

- Müşteri–Property ilişkili lead oluşturma
- Lead durumu takibi
- Bütçe ve takip tarihi yönetimi
- Not ekleme

### Property (İlan) Yönetimi

- İlan ekleme / düzenleme / silme
- Aktif / Pasif durum takibi
- Gelişmiş filtreleme (Advanced Filters)
- Fiyat, alan, şehir, tip, kategori bazlı filtreler

### Advanced Filters

- Açılır/kapanır filtre paneli
- Aktif filtre sayısı gösterimi
- Mobil & tablet uyumlu tasarım


### Dashboard

- Genel istatistikler
- Hızlı erişim panelleri

### UI / UX

- Angular Material
- Responsive (Desktop / Tablet / Mobile)
- Temiz, kurumsal tasarım
- Profesyonel admin panel deneyimi



## Kullanılan Teknolojiler

### Frontend
- Angular 15+
- Angular Material
- Reactive Forms
- RxJS
- SCSS

### Backend

- ASP.NET Core Web API
- Entity Framework Core
- Repository Pattern
- JWT Authentication


### Database
- Microsoft SQL Server



## Proje Yapısı (Frontend)

```
src/
 ├── app/
 │   ├── core/           # Guards, Interceptors, Services
 │   ├── features/       # Customers, Leads, Properties
 │   ├── layout/         # Navbar, Sidebar
 │   ├── shared/         # Reusable components
 │   └── app.module.ts
 ├── assets/
 └── styles.scss
```

---

## Kurulum

### Frontend

```bash
npm install
ng serve
```

Uygulama varsayılan olarak:

```
http://localhost:4200
```

### Backend

```bash
dotnet restore
dotnet run
```

---

## Ortam Değişkenleri

Backend `appsettings.json` içinde:

```json
"Jwt": {
  "Key": "your-secret-key",
  "Issuer": "RealEstateCRM",
  "Audience": "RealEstateCRMUsers"
}
```

---

## Responsive Desteği

- Desktop
- Tablet
- Mobile

Tüm tablolar, formlar ve toolbar bileşenleri **mobil uyumlu** olacak şekilde optimize edilmiştir.

---

##  Geliştirme Notları

- Component bazlı mimari
- Temiz ve sürdürülebilir kod
- Tekrar kullanılabilir UI bileşenleri
- Global stiller `styles.scss` üzerinden yönetilir



## Geliştirici

**Eren Mülkoğlu - Senior Software Engineer**

Full Stack .NET & Angular Developer

---

## Lisans

Bu proje eğitim ve portföy amaçlı sıfırdan geliştirilmiştir. 


> Bu proje, gerçek hayat CRM ihtiyaçları göz önünde bulundurularak **kurumsal standartlarda** geliştirilmiştir.


