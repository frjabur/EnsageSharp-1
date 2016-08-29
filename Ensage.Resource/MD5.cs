using System;
using System.Text;
using System.IO;

namespace Ensage.Resource
{
    partial class MD5
    {

        /// <summary>
        /// Расчитывает MD5 для указанного файла или возвращает null если ошибка
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns>MD5 хэш</returns>
        public static string ByFile(string filename)
        {
            string result = null;

            try
            {
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        result = Encoding.Default.GetString(md5.ComputeHash(stream));
                        stream.Close();
                        md5.Dispose();
                    }
                }
            } catch (Exception) {

            }

            return result;

        }

    }




}
