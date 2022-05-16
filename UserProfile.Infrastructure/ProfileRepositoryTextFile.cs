using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Common;
using UserProfile.Common.Helpers;
using UserProfile.Infrastructure.DBModel;

namespace UserProfile.Infrastructure
{
    public class ProfileRepositoryTextFile : IRepository<DbProfileTextFile> 
    {
        string _profileDirectory;

        public string ProfileDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_profileDirectory))
                    _profileDirectory = IOHelper.GetProfilesPath();

                return _profileDirectory;
            }
        }

        public OperationResult Delete(string fileName)
        {
            string fileFullPath = Path.Combine(ProfileDirectory, $"{fileName.Trim()}.txt");
            var result = new OperationResult() { Code = CodeResult.Error };

            FileInfo fileInf = new FileInfo(fileFullPath);

            if (!fileInf.Exists)
            {
                result.Message = $"Файл {fileFullPath} не существует";
                return result;
            }

            try
            {
                fileInf.Delete();
                result.Code = CodeResult.Success;
                result.Message = $"Файл {fileFullPath} удален успешно";
            }
            catch (Exception ex)
            {
                result.Message = LogHelper.GetErrorMessage(fileFullPath, ex);
            }

            return result;
        }

        public OperationResult Create(string fileName, DbProfileTextFile text)
        {
            var result = new OperationResult() { Code = CodeResult.Error };
            string fileFullPath = Path.Combine(ProfileDirectory, fileName);

            try
            {
                if (!Directory.Exists(ProfileDirectory))
                    Directory.CreateDirectory(ProfileDirectory);

                int filesCount = (from fl in Directory.GetFiles(ProfileDirectory)
                                  where fl.StartsWith(fileName)
                                  select fl).Count();

                string newFileName = $"{ProfileDirectory}\\{fileName}_{filesCount.ToString()}.txt";

                // полная перезапись файла 
                using (StreamWriter writer = new StreamWriter(newFileName))
                    writer.WriteLine(text.Text);

                result.Code = CodeResult.Success;
            }
            catch (Exception ex)
            {
                result.Message = LogHelper.GetErrorMessage(fileFullPath, ex);
            }

            return result;
        }

        public OperationResult Update(string fileName, DbProfileTextFile text)
        {
            string fileFullPath = Path.Combine(ProfileDirectory, fileName);
            var delRes = Delete(fileFullPath);

            if (delRes.Code == CodeResult.Success)
                return Create(fileFullPath, text);

            return delRes;
        }
        public OperationResult<List<DbProfileTextFile>> GetProfiles(string fileName = null)
        {
            var result = new OperationResult<List<DbProfileTextFile>>() { Code = CodeResult.Error };

            if (!Directory.Exists(ProfileDirectory))
            {
                result.Message = $"Каталог {ProfileDirectory} не существует";
                return result;
            }

            FileAttributes attr = File.GetAttributes(ProfileDirectory);
            if (!attr.HasFlag(FileAttributes.Directory))
            {
                result.Message = "Указано имя файла, а не каталога";
                return result;
            }

            string[] files = string.IsNullOrEmpty(fileName)
                ? Directory.GetFiles(ProfileDirectory)
                : (from fl in Directory.GetFiles(ProfileDirectory)
                   where fl.Contains(fileName)
                   select fl).ToArray();

            try
            {
                result.Data = new List<DbProfileTextFile>();

                foreach (var file in files)
                {
                    var profileAttr = file.Replace(".txt", string.Empty).Split('_');
                    string text;
                    using (StreamReader reader = new StreamReader($"{file}"))
                        text = reader.ReadToEnd();

                    result.Data.Add(new DbProfileTextFile() { ProfileFileName = profileAttr[0], CreateDate = profileAttr[1], Text = text });
                    result.Code = CodeResult.Success;
                }
            }
            catch (Exception ex)
            {
                result.Message = LogHelper.GetErrorMessage(ProfileDirectory, ex);
            }

            return result;
        }
        public OperationResult<List<string>> FindProfiles(bool isTodayList = true)
        {
            var result = new OperationResult<List<string>>() { Code = CodeResult.Error };

            if (!Directory.Exists(ProfileDirectory))
            {
                result.Message = $"Каталог {ProfileDirectory} не существует";
                return result;
            }

            FileAttributes attr = File.GetAttributes(ProfileDirectory);
            if (!attr.HasFlag(FileAttributes.Directory))
            {
                result.Message = "Указано имя файла, а не каталога";
                return result;
            }

            if (isTodayList)
            {
                result.Data = Directory.GetFiles(ProfileDirectory).ToList();
                result.Code = CodeResult.Success;
            }
            else
            {
                var directoryInfo = new DirectoryInfo(ProfileDirectory);
                result.Data = (from fl in directoryInfo.GetFiles()
                               where fl.CreationTime.Date == DateTime.Today
                               select fl.Name).ToList();

                result.Code = CodeResult.Success;
            }

            return result;
        }

    }
}
