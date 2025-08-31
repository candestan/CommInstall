# CommInstall - Modern Kurulum Oluşturucu

[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.6.2-blue.svg)](https://dotnet.microsoft.com/download/dotnet-framework)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-Windows-blue.svg)](https://www.microsoft.com/windows)

[🇹🇷 Türkçe](README_TR.md) | [🇺🇸 English](README.md)

---

## 🚀 Genel Bakış

CommInstall, Windows uygulamaları için modern ve özellik açısından zengin bir kurulum oluşturucudur. WPF ve .NET Framework 4.6.2 ile geliştirilmiş olup, gelişmiş özelliklerle profesyonel kurulum dosyaları oluşturmak için sezgisel bir arayüz sunar.

## ✨ Özellikler

### 🔧 Temel Özellikler
- **Modern WPF Arayüzü** - Karanlık/açık tema desteği ile güzel, animasyonlu kullanıcı arayüzü
- **Modüler Tasarım** - Farklı ihtiyaçlar için yapılandırılabilir kurulum modülleri
- **Çoklu Dil Desteği** - Topluluk katkısı desteği ile İngilizce ve Türkçe
- **Proje Yönetimi** - Kurulum projelerini oluşturma, kaydetme ve yükleme
- **Gerçek Zamanlı Önizleme** - Yapılandırırken değişiklikleri anında görme

### 📦 Kurulum Modülleri

| Modül | Açıklama | Durum |
|-------|----------|-------|
| 📁 **Dosya Kurulumu** | Gelişmiş seçeneklerle dosya ve klasör kurulumu | ✅ Aktif |
| 🔧 **Kayıt Defteri Anahtarları** | Windows kayıt defteri değişikliklerini yapılandırma | ✅ Aktif |
| 🔗 **Dosya Uzantıları** | Dosya türlerini uygulamanızla ilişkilendirme | ✅ Aktif |
| 📌 **Kısayollar** | Masaüstü ve başlat menüsü kısayolları oluşturma | ✅ Aktif |
| 📄 **EULA/Lisans** | Lisans anlaşmalarını görüntüleme ve yönetme | ✅ Aktif |
| 🗑️ **Kaldırma Desteği** | Kaldırma seçeneklerini yapılandırma | ✅ Aktif |
| 🚀 **Otomatik Başlatma** | Uygulama otomatik başlatma seçeneklerini ayarlama | ✅ Aktif |

### 🎨 Kullanıcı Arayüzü Özellikleri
- **Karanlık/Açık Tema** - Ayarlar kalıcılığı ile tema değiştirme
- **Özel Başlık Çubuğu** - Modern, yuvarlatılmış pencere tasarımı
- **Kart Tabanlı Düzen** - Temiz, organize modül yapılandırması
- **Duyarlı Tasarım** - Farklı ekran boyutlarına uyum
- **Yumuşak Animasyonlar** - Profesyonel kullanıcı deneyimi

## 🛠️ Kurulum

### Ön Gereksinimler
- Windows 10/11 (x64)
- .NET Framework 4.6.2 veya üzeri
- Visual Studio 2022 (geliştirme için)

### İndirme
1. [Sürümler](https://github.com/candestan/CommInstall/releases) sayfasına gidin
2. En son `CommInstallBuilder.exe` dosyasını indirin
3. Çalıştırılabilir dosyayı çalıştırın

### Kaynak Koddan Derleme
```bash
git clone https://github.com/candestan/CommInstall.git
cd CommInstall
# CommInstall.sln dosyasını Visual Studio'da açın
# Derleyin ve çalıştırın
```

## 🚀 Hızlı Başlangıç

### 1. Uygulamayı Başlatma
- `CommInstallBuilder.exe` dosyasını çalıştırın
- Açılış ekranının tamamlanmasını bekleyin
- Proje sihirbazı açılacak

### 2. Yeni Proje Oluşturma
- "Yeni Proje Oluştur" butonuna tıklayın
- Proje detaylarını girin
- "Proje Oluştur" butonuna tıklayın

### 3. Modülleri Yapılandırma
- Sol panelden modülleri seçin
- Her modülün ayarlarını yapılandırın
- Gerçek zamanlı önizleme ve özeti görün

### 4. Kurulum Dosyasını Oluşturma
- "Kurulum Oluştur" butonuna tıklayın
- Çıktı konumunu seçin
- Kurulum dosyanızı oluşturun

## 📁 Proje Yapısı

```
CommInstall/
├── CommInstallBuilder/          # Ana WPF uygulaması
│   ├── Pages/Modules/          # Kurulum modülleri
│   │   ├── FileModule/         # Dosya kurulumu
│   │   ├── RegistryModule/     # Kayıt defteri yapılandırması
│   │   ├── ExtensionModule/    # Dosya ilişkilendirmeleri
│   │   ├── ShortcutModule/     # Kısayol oluşturma
│   │   ├── EulaModule/         # Lisans yönetimi
│   │   ├── UninstallModule/    # Kaldırma seçenekleri
│   │   └── AutoStartModule/    # Otomatik başlatma yapılandırması
│   ├── App.xaml                # Uygulama giriş noktası
│   ├── MainWindow.xaml         # Ana uygulama penceresi
│   ├── ProjectWizard.xaml      # Proje oluşturma sihirbazı
│   └── SettingsWindow.xaml     # Uygulama ayarları
├── CommInstallStub/            # Kurulum stub uygulaması
├── Localization/               # Dil dosyaları
│   └── Languages/
│       ├── en.json             # İngilizce çeviriler
│       └── tr.json             # Türkçe çeviriler
└── LICENSE                     # MIT Lisansı
```

## 🔧 Yapılandırma

### Dil Ayarları
- Ayarlar → Dil bölümüne gidin
- İngilizce ve Türkçe arasında seçim yapın
- Değişiklikler anında uygulanır
- Ayarlar otomatik olarak kaydedilir

### Tema Ayarları
- Ayarlar → Tema bölümüne gidin
- Karanlık ve Açık temalar arasında geçiş yapın
- Mor vurgu rengi ile gri arka planlar
- Modern, profesyonel görünüm

### Modül Yapılandırması
Her modül şunları sağlar:
- **Gerçek Zamanlı Özet** - Yapılandırma genel bakışını görme
- **Gelişmiş Seçenekler** - Kurulum davranışını hassas ayarlama
- **Dosya Tarama** - Dosya ve klasör seçimi
- **Doğrulama** - Uygun yapılandırmayı sağlama

## 🌐 Yerelleştirme

### Desteklenen Diller
- 🇺🇸 **İngilizce** - Varsayılan dil
- 🇹🇷 **Türkçe** - Tam çeviri desteği

### Yeni Dil Ekleme
1. Repository'yi fork edin
2. `Localization/Languages/` klasöründe yeni dil dosyası oluşturun
3. Mevcut dosyalardan JSON yapısını takip edin
4. Pull request gönderin

### Dil Dosyası Yapısı
```json
{
  "Common": {
    "Save": "Kaydet",
    "Cancel": "İptal"
  },
  "MainWindow": {
    "Title": "CommInstall Builder"
  }
}
```

## 🎯 Kullanım Alanları

### Yazılım Geliştiriciler
- Uygulamalar için profesyonel kurulum dosyaları oluşturma
- Dosya ilişkilendirmelerini ve kayıt defteri anahtarlarını yapılandırma
- Otomatik başlatma ve kısayol seçeneklerini ayarlama

### Sistem Yöneticileri
- Uygulamaları ağlar genelinde dağıtma
- Kurulum prosedürlerini standartlaştırma
- Uygulama yapılandırmalarını yönetme

### Son Kullanıcılar
- Basit, rehberli kurulum süreci
- Net lisans anlaşmaları
- Kolay kaldırma

## 🚧 Geliştirme

### Derleme
```bash
# Debug derleme
msbuild CommInstall.sln /p:Configuration=Debug

# Release derleme
msbuild CommInstall.sln /p:Configuration=Release
```

### Bağımlılıklar
- **Newtonsoft.Json** - Dil dosyaları için JSON ayrıştırma
- **System.Windows.Forms** - Dosya iletişim kutuları ve sistem entegrasyonu
- **WPF** - Kullanıcı arayüzü çerçevesi

### Mimari
- **MVVM Deseni** - Temiz endişe ayrımı
- **Modüler Tasarım** - Yeni modüllerle kolay genişletme
- **Olay Tabanlı** - Duyarlı kullanıcı arayüzü
- **Kaynak Yönetimi** - Verimli tema ve dil değiştirme

## 🤝 Katkıda Bulunma

Katkılarınızı bekliyoruz! İşte nasıl yardımcı olabileceğiniz:

### 🐛 Hata Bildirme
1. Mevcut sorunları kontrol edin
2. Detaylı açıklama ile yeni sorun oluşturun
3. Yeniden üretme adımlarını ekleyin
4. Mümkünse hata günlüklerini ekleyin

### 💡 Özellik Önerme
1. Özellik isteği sorunu açın
2. Kullanım senaryosunu açıklayın
3. Mümkünse örnekler sağlayın

### 🔧 Kod Gönderme
1. Repository'yi fork edin
2. Özellik dalı oluşturun
3. Değişikliklerinizi yapın
4. Uygunsa test ekleyin
5. Pull request gönderin

### 🌐 Çeviri Ekleme
1. Dil dosyası oluşturun
2. Tüm dizeleri çevirin
3. Uygulamada test edin
4. Pull request gönderin

## 📄 Lisans

Bu proje MIT Lisansı altında lisanslanmıştır - detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 🙏 Teşekkürler

- **WPF Topluluğu** - Kullanıcı arayüzü çerçevesi ve en iyi uygulamalar
- **Newtonsoft.Json** - JSON ayrıştırma kütüphanesi
- **Katkıda Bulunanlar** - CommInstall'ı geliştirmeye yardımcı olan herkes

## 📞 Destek

- **GitHub Sorunları** - [Hataları bildirin ve özellik isteyin](https://github.com/candestan/CommInstall/issues)
- **Tartışmalar** - [Topluluk tartışmalarına katılın](https://github.com/candestan/CommInstall/discussions)
- **Wiki** - [Belgeler ve rehberler](https://github.com/candestan/CommInstall/wiki)

## 🔄 Sürüm Geçmişi

### Sürüm 1.0.0
- ✅ İlk sürüm
- ✅ Tüm temel modüller uygulandı
- ✅ Karanlık/açık tema desteği
- ✅ İngilizce ve Türkçe yerelleştirme
- ✅ Modern WPF arayüzü
- ✅ Proje yönetim sistemi

---

**CommInstall Ekibi tarafından ❤️ ile yapıldı**

[⬆️ Başa Dön](#comminstall---modern-kurulum-oluşturucu)
