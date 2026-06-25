using System;


namespace UCPPABD
{
    [Serializable]
    public class DataJadwal
    {
        public string Hari { get; set; }
        public string JamMulai { get; set; }
        public string JamSelesai { get; set; }
        public string IdKelas { get; set; }
        public string MataPelajaran { get; set; }
        public string NamaGuru { get; set; }
    }
}