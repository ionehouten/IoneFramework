using System;

namespace Ione.Framework.Core.Helpers
{
    /// <summary>
    /// NumericHelper
    /// </summary>
    public static class NumericHelper
    {
        /// <summary>
        /// IsNumeric
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumeric(this Object input)
        {
            if (input == null || input is DateTime)
                return false;

            if (input is Int16 || input is Int32 || input is Int64 || input is Decimal || input is Single || input is Double || input is Boolean)
                return true;

            try
            {
                if (input is string)
                    Double.Parse(input as string);
                else
                    Double.Parse(input.ToString());
                return true;
            }
            catch (Exception ex)
            {

                ex.SaveLog();

            }
            return false;
        }
        /// <summary>
        /// IsPositive
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPositive(this decimal? number)
        {
            return number > 0;
        }
        /// <summary>
        /// IsNegative
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsNegative(this decimal? number)
        {
            return number < 0;
        }
        /// <summary>
        /// IsZero
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsZero(this decimal? number)
        {
            return number == 0;
        }
        /// <summary>
        /// IsAwesome
        /// </summary>
        /// <param name="number"></param>
        /// <returns>ohh</returns>
        public static bool IsAwesome(this decimal? number)
        {
            return IsNegative(number) && IsPositive(number) && IsZero(number);
        }
    }

    /// <summary>
    /// Terbilang
    /// </summary>
    public class Terbilang
    {
        private ulong input = 0;
        private string output = "";
        private readonly string[] angka = { "Nol ", "Se", "Dua ", "Tiga ", "Empat ", "Lima ", "Enam ", "Tujuh ", "Delapan ", "Sembilan " };
        private readonly string[] lipat2 = { "", "puluh ", "ratus " };
        private readonly string[] lipat3 = { "", "Ribu ", "Juta ", "Milyar ", "Trilyun ", "Bilyun " };
        private string err = "";
        
        /// <summary>
        /// Metode setAngka sebagai filter terhadap angka, yang akan diisi ke variabel input, dan juga sebagai pemanggil metode Proses().
        /// </summary>
        /// <param name="angka">angka</param>
        public void SetAngka(ulong angka)
        {
            if (angka < 1e+19)
            { 
                //Pembatasan variabel angka tidak melebihi bilyun.
                input = angka;
                this.Proses();
            }
            else {
                err = "Angka melebihi batas";
            }
        }
        /// <summary>
        /// ToText
        /// </summary>
        /// <param name="angka"></param>
        /// <returns></returns>
        public string ToText(ulong angka)
        {
            if (angka < 1e+19)
            { // Pembatasan variabel angka tidak melebihi bilyun.
                input = angka;
                this.Proses();
            }
            else
            {
                err = "Angka melebihi batas";
            }
            if (err != "") return err;
            else return output;
        }
        /// <summary>
        /// Hasil dari penerjemahan angka dikeluarkan.
        /// </summary>
        /// <returns></returns>
        public string Hasil()
        {
            if (err != "") return err;
            else return output;
        }

        private void Proses()
        {
            if (input == 0)
            {
                output = angka[input];
                return;
            }
            uint temp = 0;
            ulong pangkat = 0;
            string str = "";

            for (int i = 5; i > 0; i--)
            {
                pangkat = (ulong)System.Math.Pow(10, i * 3);
                temp = (uint)(input / pangkat);
                if (temp > 0)
                {
                    if (temp == 1 && i == 1) str = "Se";
                    else str = Ratusan(temp);
                    output += str + lipat3[i];
                }
                input -= (ulong)temp * (ulong)pangkat;
            }
            output += Ratusan((uint)input);
        }

        private string Ratusan(uint rts)
        {
            uint tmp = 0;
            uint bagi = 0;
            string bil = "";
            for (int j = 2; j > 0; j--)
            {
                bagi = (uint)System.Math.Pow(10, j);
                tmp = rts / bagi;
                if (tmp > 0)
                {
                    if (tmp == 1 && j == 1)
                    {
                        rts -= tmp * bagi;
                        if (rts >= 1) bil += angka[rts] + "belas ";
                        else bil += "Sepuluh ";

                        return bil;
                    }
                    bil += angka[tmp] + lipat2[j];
                    rts -= tmp * bagi;
                }
            }
            if (rts == 1) bil += "Satu ";
            else bil += angka[rts];
            return bil;
        }

    }
}
