using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Entities.DTO;
using Sede_Checker.Help;

namespace Sede_Checker.Implementation.Services.Storage
{
    public class JsonStorageService<TData> : IStorageService<TData>
    {
        private TData _d;

        private readonly string _jsonFilePath;

        private string _StorageSalt = "3CA1C726-F884-4DA2-B181-A4A4F82E2A7A";

        public JsonStorageService(string filePath)
        {
            _jsonFilePath = filePath;
        }

        public TData GetData()
        {
            if (ReferenceEquals(_d, null))
            {
                _d = ReadFromFile();
            }

            return _d;
        }

        public bool UpdateData(TData data)
        {
            if (ReferenceEquals(data, null)) return false;

            _d = data;

            return SaveToFile(_d);
        }

        private TData ReadFromFile()
        {
            try
            {
                if (!File.Exists(_jsonFilePath))
                {   
                    this.SaveToFile(default(TData));
                }

                string json;

                using (var f = new FileStream(_jsonFilePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite))
                {
                    using (var r = new StreamReader(f, Encoding.UTF8))
                    {
                        json = r.ReadToEnd();
                    }
                }

                var data = JsonConvert.DeserializeObject<SedeRootDto>(json);

                if (ReferenceEquals(data, null))
                {
                    File.Delete(_jsonFilePath);
                    return default(TData);
                }

                var md5 = EncryptionProvider.MD5($"{data.EncryptedData}_{_StorageSalt}");

                if (md5.Equals(data.Md5))
                {
                    var rj = EncryptionProvider.DecryptText(data.EncryptedData, _StorageSalt);
                    return JsonConvert.DeserializeObject<TData>(rj);
                }

                //todo: Implement safety removing of data file
                File.Delete(_jsonFilePath);
                return default(TData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private bool SaveToFile(TData data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var aes = EncryptionProvider.EncryptText(json, _StorageSalt);
                var md5 = EncryptionProvider.MD5($"{aes}_{_StorageSalt}");

                var j = new SedeRootDto
                {
                    EncryptedData = aes,
                    Md5 = md5
                };

                var rj = JsonConvert.SerializeObject(j);

                using (var f = new FileStream(_jsonFilePath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.ReadWrite))
                {
                    using (var r = new StreamWriter(f, Encoding.UTF8))
                    {
                        r.Write(rj);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                //add logger here
                throw;
            }
        }
    }
}