using System;
using System.IO;

using Sede_Checker.Abstract.Interfaces;

namespace Sede_Checker.Implementation.Services.FileService
{

    class FileService : IFileSystemService
    {

        private string defaultDir { get; set; }

        private ILogger Logger;

        public FileService(ILogger logger)
        {
            defaultDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Sede/";

            Logger = logger;
        }

        public bool Create(string file, string data)
        {
            using (StreamWriter s = new StreamWriter($"{defaultDir}{file}"))
            {
                try
                {
                    s.Write(data);
                    return true;
                }
                catch (IOException e)
                {
                    this.Logger.Error($"System.IO exception: cannot write to file {file} : {e.Message}");
                    return false;
                }
            }
        }

        public bool Delete(string file)
        {
            try
            {
                File.Delete(file);
                return true;
            }
            catch (UnauthorizedAccessException e)
            {
                this.Logger.Error($"Unauthorized access error: cannot delete directory {file} : {e.Message}");
                return false;
            }
            catch (ArgumentNullException e)
            {
                this.Logger.Error($"Argument null exception detected: cannot delete directory {file} : {e.Message}");
                return false;
            }
            catch (ArgumentException e)
            {
                this.Logger.Error($"Argument error: cannot delete directory {file} : {e.Message}");
                return false;
            }
            catch (PathTooLongException e)
            {
                this.Logger.Error($"Too long dir name presented: cannot delete directory {file} : {e.Message}");
                return false;
            }
            catch (DirectoryNotFoundException e)
            {
                this.Logger.Error($"Cannot find directory {file} : {e.Message}");
                return false;
            }
            catch (IOException e)
            {
                this.Logger.Error($"System.IO error: cannot delete directory {file} : {e.Message}");
                return false;
            }
        }

        public bool CreateDirectory(string directory)
        {
            if (!Directory.Exists($"{defaultDir}{directory}"))
            {
                try
                {
                    Directory.CreateDirectory($"{defaultDir}{directory}");
                    return true;
                }
                catch (UnauthorizedAccessException e)
                {
                    this.Logger.Error($"Unauthorized access error: cannot create directory {defaultDir}{directory} : {e.Message}");
                    return false;
                }
                catch (ArgumentNullException e)
                {
                    this.Logger.Error($"Argument null exception detected: cannot create directory {defaultDir}{directory} : {e.Message}");
                    return false;
                }
                catch (ArgumentException e)
                {
                    this.Logger.Error($"Argument error: cannot create directory {defaultDir}{directory} : {e.Message}");
                    return false;
                }
                catch (PathTooLongException e)
                {
                    this.Logger.Error($"Too long dir name presented: cannot create directory {defaultDir}{directory} : {e.Message}");
                    return false;
                }
                catch(DirectoryNotFoundException e)
                {
                    this.Logger.Error($"Cannot find directory {defaultDir}{directory} : {e.Message}");
                    return false;
                }
                catch (IOException e)
                {
                    this.Logger.Error($"System.IO error: cannot create directory {defaultDir}{directory} : {e.Message}");
                    return false;
                }
            }
            return true;
        }

        public string Load(string file)
        {
            if (!File.Exists(file))
            {
                this.Logger.Error($"Error detected, cannot find file {file}!");
                return null;
            }
            using(StreamReader s = new StreamReader(file))
            {
                try
                {
                    var d = s.ReadToEnd();
                    return d;
                }
                catch (IOException e)
                {
                    this.Logger.Error($"Error detected, cannot read file {file}, {e.Message}");
                    return null;
                }
            }
        }
    }
}
