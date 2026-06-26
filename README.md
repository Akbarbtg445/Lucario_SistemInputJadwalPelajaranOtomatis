# 📚 Sistem Input Jadwal Pelajaran Otomatis

Aplikasi desktop berbasis **Windows Forms (C#)** untuk mengelola jadwal pelajaran sekolah secara otomatis. Dibangun menggunakan Visual Studio dengan koneksi database MySQL dan laporan menggunakan SAP Crystal Reports.

---

## 🖥️ Tampilan Aplikasi

### 1. Form Login
Pengguna dapat login sebagai **Admin** atau **Siswa** menggunakan username dan password yang terdaftar di database.

### 2. Dashboard Administrator
Admin dapat mengelola seluruh data jadwal pelajaran, termasuk:
- Input jadwal berdasarkan hari, kelas, mata pelajaran, guru, jam mulai dan jam selesai
- Melihat statistik jumlah jadwal per hari dalam bentuk grafik batang
- Import data guru dari file Excel
- Tambah, ubah, hapus, dan cetak jadwal
- Melihat daftar jadwal aktif secara lengkap

### 3. Dashboard Siswa
Siswa dapat:
- Mencari jadwal berdasarkan kelas
- Melihat jadwal pelajaran lengkap (hari, jam, mata pelajaran, guru)
- Mengatur preferensi waktu pelajaran
- Melihat kapasitas dan sisa kuota kelas
- Mencetak jadwal pelajaran pribadi

### 4. Laporan
Laporan jadwal pelajaran dapat dicetak dalam format Crystal Reports, menampilkan informasi lengkap jadwal beserta tanggal cetak.

---

## ⚙️ Teknologi yang Digunakan

| Teknologi | Keterangan |
|---|---|
| C# Windows Forms | Bahasa pemrograman dan framework UI |
| Visual Studio 2022 | IDE pengembangan |
| MySQL | Database penyimpanan data |
| SAP Crystal Reports | Cetak laporan |
| Microsoft Excel | Import data guru |

---

## 🚀 Cara Instalasi

### Prasyarat
- Visual Studio 2022
- MySQL Server
- SAP Crystal Reports Runtime untuk Visual Studio
- .NET Framework 4.7.2 atau lebih baru

### Langkah-langkah

**1. Clone repository ini:**
```bash
git clone https://github.com/Akbarbtg445/Lucario_SistemInputJadwalPelajaranOtomatis.git
```

**2. Import database:**
- Buka MySQL dan jalankan file `database_setup.sql` yang tersedia di root folder

**3. Konfigurasi koneksi database:**
- Buka aplikasi, klik **Atur Server Database** pada form login
- Masukkan host, port, username, dan password MySQL kamu

**4. Buka project di Visual Studio:**
- Buka file `UCPPABD.sln`
- Build solution (Ctrl+Shift+B)
- Jalankan aplikasi (F5)

---

## 👤 Akun Default

| Role | Username |
