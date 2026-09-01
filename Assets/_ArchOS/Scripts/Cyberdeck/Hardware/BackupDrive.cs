//using NUnit.Framework;
//using System.Collections.Generic;
//using UnityEngine;

//namespace ArchOS
//{
//    public class BackupDrive : Hardware
//    {
//        private readonly List<Program> _programBackup;

//        public BackupDrive(ModuleSO data) : base(data)
//        {
//            _programBackup = new List<Program>();
//        }

//        public override void Install()
//        {
//            _programBackup.Clear();

//        }

//        public override void Uninstall()
//        {
            
//            _programBackup?.Clear();
//        }

//        public void SaveProgram(Program program)
//        {
//            _programBackup.Add(program);
//        }
//    }
//}
