Skenario Implementasi SQL Injection (UCP 2)

Pada aplikasi ini, kerentanan SQL Injection sengaja diimplementasikan pada **Form1 (Form Login)** untuk mendemonstrasikan celah keamanan *Authentication Bypass*.

1. Titik Kerentanan (Vulnerability Point)
Kerentanan terdapat pada logika pembentukan query di `Form1.cs` baris pengecekan login Admin. Query dibentuk menggunakan penggabungan string (*string concatenation*) tanpa sanitasi atau `Parameterized Query`, seperti berikut:
`query = "SELECT username FROM Admin WHERE username='" + user + "' AND password='" + pass + "'";`

2. Skenario Serangan (Attack Scenario)
Penyerang tidak mengetahui *username* maupun *password* Admin, tetapi ingin masuk ke dalam `AdminDashboard`.

Langkah-langkah Serangan:
1. Penyerang membuka aplikasi dan memilih Role: **Admin**.
2. Pada kolom **Username**, penyerang memasukkan *payload* injeksi berikut: 
   `' OR '1'='1`
3. Pada kolom **Password**, penyerang mengetikkan teks acak (misal: `123`).
4. Klik tombol **Login**.

3. Mengapa Ini Berhasil? (How it works)
Ketika *payload* disisipkan, struktur query di *backend* (SQL Server) akan berubah dan dieksekusi menjadi seperti ini:
`SELECT username FROM Admin WHERE username='' OR '1'='1' AND password='123'`

Karena logika `'1'='1'` adalah absolut/selalu **TRUE** (Benar), maka klausa WHERE akan mengabaikan pengecekan *password*. SQL Server akan mengembalikan baris pertama dari tabel Admin, sehingga penyerang berhasil *login* tanpa kredensial yang sah.

### 4. Pencegahan (Mitigation) - Defense Strategy
Untuk memperbaiki celah ini di dunia nyata, pengembang harus menggunakan **Parameterized Queries** dari ADO.NET (seperti `cmd.Parameters.AddWithValue("@user", user);`), sehingga input dari pengguna akan diperlakukan murni sebagai "nilai data" dan bukan sebagai "perintah SQL".
update 1  
update 2  
update 3  
update 4  
update 5  
update 6  
update 7  
update 8  
update 9  
update 10  
update 11  
update 12  
update 13  
update 14  
update 15  
update 16  
update 17  
update 18  
update 19  
update 20  
update 21  
update 22  
update 23  
