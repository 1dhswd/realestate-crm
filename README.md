# RealEstate CRM

Modern, ölçeklenebilir ve kullanıcı dostu bir Gayrimenkul CRM (Customer Relationship Management) uygulaması. Proje; emlak ofisleri ve bireysel danışmanların müşteri, ilan (property) ve lead süreçlerini tek bir panelden profesyonel şekilde yönetebilmesi için geliştirilmiştir.

(A modern, scalable, and user-friendly Real Estate CRM (Customer Relationship Management) application. The project was developed to enable real estate agencies and individual consultants to professionally manage client, property, and lead processes from a single dashboard.)


<img width="1890" height="862" alt="image" src="https://github.com/user-attachments/assets/1f1ac399-c84b-475e-9198-9f9c99a29d1e" />
<img width="1899" height="675" alt="image" src="https://github.com/user-attachments/assets/9a5cde4f-e1d4-4cac-b91e-b3012e93a596" />
<img width="1923" height="614" alt="image" src="https://github.com/user-attachments/assets/732f1795-80be-4bf3-9ccf-5bb4ffd1480f" />
<img width="1894" height="652" alt="image" src="https://github.com/user-attachments/assets/2ace668c-73d9-44db-9a08-766c129c0991" />
<img width="1907" height="966" alt="image" src="https://github.com/user-attachments/assets/de59feb0-46e9-4b3c-a66f-ce97f19ef8ac" />
<img width="1901" height="998" alt="image" src="https://github.com/user-attachments/assets/aa218c11-42a9-49ac-8f28-7d5a9057ee59" />
<img width="785" height="909" alt="image" src="https://github.com/user-attachments/assets/eb44e3c1-507c-49f3-a3ff-c9aead9ff87b" />



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


## Proje Yapısı (Backend)

```

RealEstateCRM
├── RealEstateCRM.API
│   ├── Controllers
│   │   ├── AuthController.cs
│   │   ├── CustomerController.cs
│   │   └── PropertyController.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── Middleware
│       └── ExceptionMiddleware.cs
│
├── RealEstateCRM.Application
│   ├── Features
│   │   ├── Auth
│   │   │   ├── Commands
│   │   │   └── Queries
│   │   ├── Customers
│   │   │   ├── Commands
│   │   │   └── Queries
│   │   └── Properties
│   │       ├── Commands
│   │       └── Queries
│   │
│   ├── DTOs
│   ├── Interfaces
│   │   ├── Repositories
│   │   └── Services
│   ├── Behaviors
│   │   └── ValidationBehavior.cs
│   └── Mappings
│       └── AutoMapperProfile.cs
│
├── RealEstateCRM.Domain
│   ├── Entities
│   │   ├── Customer.cs
│   │   ├── Property.cs
│   │   └── User.cs
│   ├── Enums
│   ├── ValueObjects
│   └── Common
│       └── BaseEntity.cs
│
├── RealEstateCRM.Persistence
│   ├── Context
│   │   └── RealEstateDbContext.cs
│   ├── Configurations
│   │   └── CustomerConfiguration.cs
│   ├── Repositories
│   └── Migrations
│
├── RealEstateCRM.Infrastructure
│   ├── Services
│   │   ├── EmailService.cs
│   │   └── TokenService.cs
│   ├── Security
│   │   └── JwtSettings.cs
│   └── Logging
│       └── SerilogConfig.cs
│
└── RealEstateCRM.sln

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


