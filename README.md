# Mentorile.com - Uçtan Uca Dijital Öğrenme Platformu

**Mentorile.com**, ortaöğretim ve yükseköğretim öğrencilerinin ders çalışma süreçlerini tamamen dijitalleştiren, modern teknolojilerle geliştirilmiş, mikroservis mimarisi tabanlı kapsamlı bir öğrenme ve gelişim platformudur.

Platform, öğrencilere kişiselleştirilmiş bir öğrenme deneyimi sunarken, eğitmenlere de içerik yönetimi ve öğrenci takibi için güçlü araçlar sağlar.

## Temel Özellikler

-   **Kişiselleştirilmiş Yol Haritaları:** Her öğrencinin seviyesine ve hedeflerine uygun çalışma planları.
-   **Akıllı Tekrar Sistemi:** Konu bazlı, unutma eğrisine göre optimize edilmiş tekrar planları.
-   **Performans Analizi:** Deneme sınavları ve soru bankaları ile detaylı ilerleme takibi.
-   **Etkileşimli Araçlar:** Not tutma, dijital `flashcard`'lar oluşturma ve içerik pekiştirme.
-   **Esnek ve Ölçeklenebilir:** Modüler mikroservis yapısı sayesinde her servis bağımsız olarak geliştirilebilir, dağıtılabilir ve ölçeklenebilir.
-   **Güçlü ve Güvenli:** Merkezi `IdentityServer` ile OAuth2.0 ve OpenID Connect protokolleri kullanılarak sağlanan güçlü kimlik doğrulama ve yetkilendirme.
-   **Yüksek Performans:** `Ocelot API Gateway` ile dinamik yönlendirme, yük dengeleme ve servis keşfi.
-   **Kesintisiz Kullanıcı Deneyimi:** Modern ASP.NET Core MVC ve bulut tabanlı görsel depolama altyapısı ile akıcı bir arayüz.

## Mimari ve Teknoloji Yaklaşımları

Proje, güncel yazılım geliştirme prensipleri ve teknolojileri temel alınarak **Mikroservis Mimarisi** ile tasarlanmıştır. Bu yaklaşım, projenin esnekliğini, bakım kolaylığını ve ölçeklenebilirliğini en üst düzeye çıkarır.

-   **CQRS (Command Query Responsibility Segregation):** `MediatR` kütüphanesi ile birlikte kullanılarak, sisteme yapılan yazma (Command) ve okuma (Query) istekleri birbirinden ayrılmış, bu sayede daha temiz ve yönetilebilir bir kod tabanı oluşturulmuştur.
-   **Design Patterns:** Yapılandırma yönetimi için `Options Pattern` gibi endüstri standardı tasarım desenleri aktif olarak kullanılmaktadır.
-   **Asenkron İletişim:** Servisler arası asenkron iletişim `RabbitMQ` mesaj kuyruk sistemi üzerinden sağlanarak sistemin dayanıklılığı ve hızı artırılmıştır.

### Teknoloji Stack'i

-   **Backend:** ASP.NET Core
-   **API Gateway:** Ocelot
-   **Kimlik Yönetimi:** IdentityServer (OAuth2.0, OpenID Connect)
-   **Veritabanı:** PostgreSQL
-   **Mesajlaşma (Message Broker):** RabbitMQ
-   **Containerization:** Docker / Docker Compose
-   **Depolama:** Bulut Tabanlı Görsel Depolama (PhotoStock servisi için)
-   **Frontend Mimarisi:** ASP.NET Core MVC (Client)

## Proje ve Servis Yapısı

Proje, her biri kendi sorumluluk alanına odaklanmış bağımsız servislerden oluşur. Bu modüler yapı, geliştirmeyi ve bakımı kolaylaştırır.

```
/Mentorile
├───/Clients
│   └───/Mentorile.Client.Web (Kullanıcı arayüzü - ASP.NET Core MVC)
├───/Gateways
│   └───/Mentorile.Gateway.API (Tüm istekerin yönetildiği Ocelot API Gateway)
├───/IdentityServer
│   └───/Mentorile.IdentityServer (Kimlik ve Yetki Yönetimi)
├───/Services
│   ├───/Basket (Sepet işlemleri servisi)
│   ├───/Course (Kurs, ders ve içerik yönetimi servisi)
│   ├───/Discount (İndirim ve kupon yönetimi servisi)
│   ├───/Order (Sipariş yönetimi servisi)
│   ├───/Payment (Ödeme işlemleri servisi)
│   ├───/PhotoStock (Görsel depolama ve yönetim servisi)
│   ├───/Study (Öğrencinin çalışma, tekrar ve ilerleme yönetimi servisi)
│   ├───/User (Kullanıcı yönetimi servisi)
│   └───/Email (E-posta gönderim servisi)
├───/Shared
│   └───/Mentorile.Shared (Tüm servisler arasında paylaşılan ortak kodlar)
└───/docker-compose.yml (Tüm projenin orkestrasyon dosyası)
```

## Kurulum ve Başlatma (Docker ile)

Proje, `docker-compose` kullanılarak kolayca ayağa kaldırılabilir. Bu sayede tüm servisler ve bağımlılıklar (PostgreSQL, RabbitMQ vb.) tek bir komutla, izole container'lar içinde çalıştırılır.

1.  **Depoyu Klonlayın:**
    ```bash
    git clone https://github.com/berkaykarakush/Mentorile.git
    cd Mentorile
    ```

2.  **Docker'ı Başlatın:**
    Makinenizde Docker Desktop'ın çalıştığından emin olun.

3.  **Projeyi Ayağa Kaldırın:**
    Projenin ana dizininde aşağıdaki komutu çalıştırın:
    ```bash
    docker-compose up -d --build
    ```
    Bu komut:
    -   Gerekli Docker imajlarını (`--build` parametresi ile) yeniden oluşturur.
    -   Tüm mikroservisler, veritabanı (PostgreSQL), mesaj kuyruğu (RabbitMQ) ve diğer bağımlılıklar için container'ları oluşturur ve başlatır.
    -   Servisler arasındaki iletişimi sağlamak için ortak bir ağ (network) oluşturur.
    -   `-d` (detached mode) parametresi, container'ların terminali meşgul etmeden arka planda çalışmasını sağlar.

4.  **Uygulamaya Erişin:**
    Servisler başladıktan sonra, `Mentorile.Client.Web` projesinin çalıştığı adrese (örn: `http://localhost:5002`) giderek uygulamayı görüntüleyebilirsiniz. Port bilgileri için `docker-compose.yml` dosyasını kontrol edebilirsiniz.

5.  **Sistemi Durdurma:**
    Uygulamayı ve tüm ilişkili container'ları durdurmak için aşağıdaki komutu kullanın:
    ```bash
    docker-compose down
    ```
    Eğer container'lar ile birlikte oluşturulan volumeları (veritabanı verileri gibi) da silmek isterseniz `-v` parametresini ekleyebilirsiniz: `docker-compose down -v`.

## Katkıda Bulunma

Bu proje sürekli olarak geliştirilmektedir. Katkıda bulunmak isterseniz, lütfen bir `pull request` açın veya bir `issue` oluşturarak fikirlerinizi paylaşın.
