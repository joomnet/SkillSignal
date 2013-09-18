using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Machine.Specifications;

namespace SkillSignal.Specifications
{
    using SkillSignal.Common;

    public class when_accessing_a_file_system
    {
        Establish context = () =>
        {
            _fileSystem = new InMemoryFileSystem();
        };

        Because of = () => _expectedException = Catch.Exception(() => _fileSystem.GetDirectories(null));

        It should_throw_an_argument_null_exception =
            () => _expectedException.ShouldBeOfType<ArgumentNullException>();

        static InMemoryFileSystem _fileSystem;
        private static Exception _expectedException;
    }

    public class when_accessing_a_file_system_and_directory_does_not_exist
    {
        Establish context = () =>
        {
            _fileSystem = new InMemoryFileSystem();
        };

        Because of = () => _expectedException = Catch.Exception(() => _fileSystem.GetDirectories("X:Directory"));

        It should_throw_an_argument_null_exception =
            () => _expectedException.ShouldBeOfType<DirectoryNotFoundException>();

        static InMemoryFileSystem _fileSystem;
        private static Exception _expectedException;
    }

    public class when_accessing_a_file_system_and
    {
        Establish context = () =>
        {
            _fileSystem = new InMemoryFileSystem();
            _fileSystem.CreateDirectory(@"C:\Development\machine.specifications");
            _fileSystem.CreateDirectory(@"C:\Development\SkillSignal");
            _fileSystem.CreateDirectory(@"C:\Development\WhatsAPINet");
        };

        Because of = () =>
        {
            _directories = _fileSystem.GetDirectories(@"C:\Development");
        };

        It should_return_a_list_of_all_directories_within_the_directory = () => _directories.Count().ShouldEqual(3);
        

        static InMemoryFileSystem _fileSystem;
        private static Exception _expectedException;
        private static string[] _directories;
    }

    internal class InMemoryFileSystem : IFileSystem
    {
        private readonly List<InMemoryDirectory> _disks = new List<InMemoryDirectory>();
        private InMemoryDirectory _inMemoryDirectory;

        public string[] GetDirectories(string path)
        {
            ObjectValidator.IfNullThrowException(path, "path");

            var dirs = path.Split('\\');
            var targetRootDisk = _disks.SingleOrDefault(d => d.Name == dirs.First());

            if (targetRootDisk == null)
            {
                throw new DirectoryNotFoundException(string.Format("Could not find part of the path {0}", path));
            }
            return _RecursiveDirectoryFind(targetRootDisk, dirs, 1).Select(x => Path.Combine(path, x.Name)).ToArray();
        }

        private List<InMemoryDirectory> _RecursiveDirectoryFind(InMemoryDirectory targetRootDisk, string[] dirs, int startIndex)
        {
            if (dirs.Count() > startIndex)
            {
                var targetChildDirectory = targetRootDisk.InMemoryDirectories.SingleOrDefault(x => x.Name == dirs[startIndex]);
                return _RecursiveDirectoryFind(targetChildDirectory, dirs, startIndex + 1);
            }
            return targetRootDisk.InMemoryDirectories;
        }

        public bool DirectoryExists(string path)
        {
            throw new NotImplementedException();
        }

        public void CreateDirectory(string path)
        {
            var dirs = path.Split('\\');
            if (!dirs.First().Contains(':')) return;

            _inMemoryDirectory = _disks.SingleOrDefault(x => x.Name == dirs.First()) ?? new InMemoryDirectory { Name = dirs.First() };
            if (_disks.All(x => x.Name != dirs.First()))
            {
                _disks.Add(_inMemoryDirectory);
            }
            _RecursiveDirectoryAdd(_inMemoryDirectory, dirs, 1);
        }

        private void _RecursiveDirectoryAdd(InMemoryDirectory inMemoryDirectory, string[] dirs, int startIndex)
        {
            if (dirs.Count() > startIndex)
            {
                InMemoryDirectory targetDirectory = inMemoryDirectory.InMemoryDirectories.SingleOrDefault(x => x.Name == dirs[startIndex]);

                if (targetDirectory == null)
                {
                    targetDirectory = new InMemoryDirectory { Name = dirs[startIndex] };
                    inMemoryDirectory.InMemoryDirectories.Add(targetDirectory);
                }
                _RecursiveDirectoryAdd(targetDirectory, dirs, startIndex + 1);
            }
        }

        public void DeleteDirectory(string path, bool recurse)
        {
            throw new NotImplementedException();
        }

        public void CopyPattern(string sourcePath, string destinationPath, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public bool FileExists(string path)
        {
            throw new NotImplementedException();
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadAllBytes(string path)
        {
            throw new NotImplementedException();
        }

        public string[] GetFiles(string path)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string path)
        {
            throw new NotImplementedException();
        }

        public string[] GetFiles(string parentDirectory, string searchPattern, SearchOption searchOptions)
        {
            throw new NotImplementedException();
        }

        public void WriteAllText(string path, string text)
        {
            throw new NotImplementedException();
        }

        public string ReadAllText(string path)
        {
            throw new NotImplementedException();
        }

        public void SetLastWriteTime(string fileName, DateTime writeTime)
        {
            throw new NotImplementedException();
        }

        public DateTime GetFileLastWriteTime(string fileName)
        {
            throw new NotImplementedException();
        }

        public void AppendLineToFile(string path, string text)
        {
            throw new NotImplementedException();
        }

        public void Copy(string sourceFile, string destFile)
        {
            throw new NotImplementedException();
        }

        public void CopyIfExists(string sourceFile, string destFile)
        {
            throw new NotImplementedException();
        }

        public long GetFileSize(string fileName)
        {
            throw new NotImplementedException();
        }

        public string[] GetFiles(string directoryToSearch, string searchPattern)
        {
            throw new NotImplementedException();
        }

        public void AppendBytes(string filePath, byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public long GetDirectorySize(string directoryPath)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDirectoryCreationTime(string directoryPath)
        {
            throw new NotImplementedException();
        }

        public Stream OpenFileStream(string filePath)
        {
            throw new NotImplementedException();
        }

        public void MoveFile(string sourceFilePath, string destinationFilePath)
        {
            throw new NotImplementedException();
        }
    }

    internal class InMemoryDirectory
    {
        protected bool Equals(InMemoryDirectory other)
        {
            return string.Equals(Name, other.Name) && Equals(InMemoryDirectories, other.InMemoryDirectories);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((InMemoryDirectory) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ (InMemoryDirectories != null ? InMemoryDirectories.GetHashCode() : 0);
            }
        }

        public static bool operator ==(InMemoryDirectory left, InMemoryDirectory right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InMemoryDirectory left, InMemoryDirectory right)
        {
            return !Equals(left, right);
        }

        public InMemoryDirectory()
        {
            InMemoryDirectories = new List<InMemoryDirectory>();
        }
        public string Name { get; set; }

        public List<InMemoryDirectory> InMemoryDirectories { get; set; }
    }

    internal interface IFileSystem
    {
        string[] GetDirectories(string directoryPath);

        bool DirectoryExists(string path);

        void CreateDirectory(string path);

        void DeleteDirectory(string path, bool recurse);

        void CopyPattern(string sourcePath, string destinationPath, string searchPattern);

        bool FileExists(string path);

        void WriteAllBytes(string path, byte[] bytes);

        byte[] ReadAllBytes(string path);

        string[] GetFiles(string path);

        void DeleteFile(string path);

        string[] GetFiles(string parentDirectory, string searchPattern, SearchOption searchOptions);

        void WriteAllText(string path, string text);

        string ReadAllText(string path);

        void SetLastWriteTime(string fileName, DateTime writeTime);

        DateTime GetFileLastWriteTime(string fileName);

        void AppendLineToFile(string path, string text);

        void Copy(string sourceFile, string destFile);

        void CopyIfExists(string sourceFile, string destFile);

        long GetFileSize(string fileName);

        string[] GetFiles(string directoryToSearch, string searchPattern);

        void AppendBytes(string filePath, byte[] bytes);

        long GetDirectorySize(string directoryPath);

        DateTime GetDirectoryCreationTime(string directoryPath);

        Stream OpenFileStream(string filePath);

        void MoveFile(string sourceFilePath, string destinationFilePath);
    }


}
