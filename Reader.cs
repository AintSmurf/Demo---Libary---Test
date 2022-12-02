using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary
{
    public class Reader
    {
        #region eunms
        public enum ReaderType
        {
            Child,
            Adult
        }


        #endregion

        #region Private Variabls
        private string firtName;
        private string lastname;
        private ReaderType type;

        #endregion

        #region Properties
        public string FirtName
        {
            get { return firtName; }
        }

        public string LastName
        {
            get { return lastname; }
        }

        public ReaderType Type
        {
            get => type;
        }
        #endregion

        #region Constructors
        public Reader(string firstName, string lastName, ReaderType type)
        {
            this.firtName = firstName;
            this.lastname = lastName;
            this.type = type;
        }
        #endregion
    }
}
