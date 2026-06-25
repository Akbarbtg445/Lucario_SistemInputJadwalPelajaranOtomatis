-- =======================================================
-- SKRIP SETUP DATABASE: UCP_PABD_Jadwal
-- =======================================================

USE master;
GO

-- Hapus database lama jika sudah ada untuk menghindari bentrok saat setup ulang
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'UCP_PABD_Jadwal')
BEGIN
    ALTER DATABASE UCP_PABD_Jadwal SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE UCP_PABD_Jadwal;
END
GO

CREATE DATABASE UCP_PABD_Jadwal;
GO

USE UCP_PABD_Jadwal;
GO

-- 1. Tabel Admin
CREATE TABLE Admin (
    username VARCHAR(50) PRIMARY KEY,
    password VARCHAR(50) NOT NULL
);

-- 2. Tabel User (Siswa)
CREATE TABLE [User] (
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(50) NOT NULL,
    nama VARCHAR(100) NOT NULL,
    NIS VARCHAR(50) PRIMARY KEY
);

-- 3. Tabel Kelas
CREATE TABLE Kelas (
    idKelas VARCHAR(50) PRIMARY KEY,
    kapasitas INT NOT NULL
);

-- 4. Tabel Guru
CREATE TABLE Guru (
    nama VARCHAR(100) PRIMARY KEY
);

-- 5. Tabel Mata Pelajaran
CREATE TABLE MataPelajaran (
    namaMapel VARCHAR(100) PRIMARY KEY
);

-- 6. Tabel Jadwal Utama
CREATE TABLE Jadwal (
    idJadwal INT IDENTITY(1,1) PRIMARY KEY,
    hari VARCHAR(20) NOT NULL,
    jamMulai TIME(0) NOT NULL,
    jamSelesai TIME(0) NOT NULL,
    idKelas VARCHAR(50) FOREIGN KEY REFERENCES Kelas(idKelas) ON DELETE CASCADE,
    MataPelajaran VARCHAR(100) FOREIGN KEY REFERENCES MataPelajaran(namaMapel) ON DELETE CASCADE,
    NamaGuru VARCHAR(100) FOREIGN KEY REFERENCES Guru(nama) ON DELETE CASCADE
);

-- 7. Tabel Jadwal Pribadi (Siswa terdaftar di jadwal tertentu)
CREATE TABLE Jadwal_Pribadi (
    idJadwalPribadi INT IDENTITY(1,1) PRIMARY KEY,
    idJadwal INT FOREIGN KEY REFERENCES Jadwal(idJadwal) ON DELETE CASCADE,
    NIS VARCHAR(50) FOREIGN KEY REFERENCES [User](NIS) ON DELETE CASCADE
);
GO

-- =======================================================
-- VIEW DATA JADWAL (vw_JadwalPelajaran)
-- =======================================================
CREATE VIEW vw_JadwalPelajaran AS
SELECT 
    idJadwal,
    hari,
    jamMulai,
    jamSelesai,
    idKelas,
    MataPelajaran,
    NamaGuru
FROM Jadwal;
GO

-- =======================================================
-- STORED PROCEDURES (sp_Insert, sp_Update, sp_Delete, dll)
-- =======================================================

-- A. Insert Jadwal
CREATE PROCEDURE sp_InsertJadwal
    @hari VARCHAR(20),
    @mulai TIME(0),
    @selesai TIME(0),
    @kelas VARCHAR(50),
    @namaMapel VARCHAR(100),
    @namaGuru VARCHAR(100)
AS
BEGIN
    INSERT INTO Jadwal (hari, jamMulai, jamSelesai, idKelas, MataPelajaran, NamaGuru)
    VALUES (@hari, @mulai, @selesai, @kelas, @namaMapel, @namaGuru);
END;
GO

-- B. Update Jadwal (Admin)
CREATE PROCEDURE sp_UpdateJadwal
    @idJadwal INT,
    @hari VARCHAR(20),
    @mulai TIME(0),
    @selesai TIME(0),
    @kelas VARCHAR(50),
    @namaMapel VARCHAR(100),
    @namaGuru VARCHAR(100)
AS
BEGIN
    UPDATE Jadwal
    SET hari = @hari,
        jamMulai = @mulai,
        jamSelesai = @selesai,
        idKelas = @kelas,
        MataPelajaran = @namaMapel,
        NamaGuru = @namaGuru
    WHERE idJadwal = @idJadwal;
END;
GO

-- C. Delete Jadwal
CREATE PROCEDURE sp_DeleteJadwal
    @idJadwal INT
AS
BEGIN
    DELETE FROM Jadwal WHERE idJadwal = @idJadwal;
END;
GO

-- D. Cari Jadwal berdasarkan Kelas
CREATE PROCEDURE sp_SearchJadwal
    @kelas VARCHAR(50)
AS
BEGIN
    SELECT * FROM vw_JadwalPelajaran WHERE idKelas = @kelas;
END;
GO

-- E. Update Jadwal (User/Siswa)
CREATE PROCEDURE sp_UpdateJadwalUser
    @idJadwal INT,
    @mulai TIME(0),
    @selesai TIME(0),
    @kelas VARCHAR(50),
    @namaMapel VARCHAR(100)
AS
BEGIN
    UPDATE Jadwal
    SET jamMulai = @mulai,
        jamSelesai = @selesai,
        idKelas = @kelas,
        MataPelajaran = @namaMapel
    WHERE idJadwal = @idJadwal;
END;
GO

-- =======================================================
-- INPUT DUMMY DATA UNTUK PENGUJIAN
-- =======================================================

-- Admin & Siswa
INSERT INTO Admin (username, password) VALUES ('admin', 'admin123');
INSERT INTO [User] (username, password, nama, NIS) VALUES ('siswa', 'siswa123', 'Siswa Budi Utomo', 'NIS001');
INSERT INTO [User] (username, password, nama, NIS) VALUES ('siswa2', 'siswa123', 'Siswa Andi', 'NIS002');

-- Kelas
INSERT INTO Kelas (idKelas, kapasitas) VALUES ('KLS01', 30);
INSERT INTO Kelas (idKelas, kapasitas) VALUES ('KLS02', 25);
INSERT INTO Kelas (idKelas, kapasitas) VALUES ('KLS03', 20);

-- Guru
INSERT INTO Guru (nama) VALUES ('Budi Handoko, M.Pd.');
INSERT INTO Guru (nama) VALUES ('Siti Aminah, S.Pd.');
INSERT INTO Guru (nama) VALUES ('Hendra Wijaya, M.T.');

-- Mata Pelajaran
INSERT INTO MataPelajaran (namaMapel) VALUES ('Matematika');
INSERT INTO MataPelajaran (namaMapel) VALUES ('Fisika');
INSERT INTO MataPelajaran (namaMapel) VALUES ('Bahasa Inggris');
INSERT INTO MataPelajaran (namaMapel) VALUES ('Kimia');

-- Jadwal
INSERT INTO Jadwal (hari, jamMulai, jamSelesai, idKelas, MataPelajaran, NamaGuru)
VALUES ('Senin', '07:30:00', '09:00:00', 'KLS01', 'Matematika', 'Budi Handoko, M.Pd.');

INSERT INTO Jadwal (hari, jamMulai, jamSelesai, idKelas, MataPelajaran, NamaGuru)
VALUES ('Selasa', '09:30:00', '11:00:00', 'KLS02', 'Fisika', 'Siti Aminah, S.Pd.');

INSERT INTO Jadwal (hari, jamMulai, jamSelesai, idKelas, MataPelajaran, NamaGuru)
VALUES ('Rabu', '13:00:00', '14:30:00', 'KLS01', 'Bahasa Inggris', 'Hendra Wijaya, M.T.');

-- Jadwal Pribadi (Siswa terdaftar di kelas)
-- Masukkan Siswa Budi ke Matematika Senin (Jadwal ID 1)
INSERT INTO Jadwal_Pribadi (idJadwal, NIS) VALUES (1, 'NIS001');
INSERT INTO Jadwal_Pribadi (idJadwal, NIS) VALUES (1, 'NIS002');

-- Masukkan Siswa Budi ke Fisika Selasa (Jadwal ID 2)
INSERT INTO Jadwal_Pribadi (idJadwal, NIS) VALUES (2, 'NIS001');
GO

PRINT 'Database UCP_PABD_Jadwal Berhasil Diinisialisasi!';
GO
