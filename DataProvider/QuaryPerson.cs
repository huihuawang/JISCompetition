using DataModels;
using System.Collections.ObjectModel;
using System.Data;

namespace DataProvider
{
    public class QuaryPerson: QuaryBase<Person>
    {
        public QuaryPerson() 
            : base(()=>new MapperPerson())
        {
        }

        public Collection<Person> GetPersons()
        {
            string cmdText = @"SELECT PersonID, Name, Mobile, Email FROM tblPerson";
            CommandType cmdType = CommandType.Text;
            Collection<IDataParameter> cmdParams = new Collection<IDataParameter>();

            //// USE THIS IF YOU ACTUALLY HAVE PARAMETERS
            //IDataParameter param1 = command.CreateParameter();
            //param1.ParameterName = "paramName 1"; // put parameter name here
            //param1.Value = 5; // put value here;

            //cmdParams.Add(param1);

            return base.ExecuteReader(cmdText, cmdType, cmdParams);
        }
       
    }
}
