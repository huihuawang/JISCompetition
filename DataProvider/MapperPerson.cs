using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataModels;

namespace DataProvider
{
    class MapperPerson: MapperBase<Person>
    {
        protected override Person Map(IDataRecord record)
        {
            try
            {
                Person p = new Person();

                p.Id = (DBNull.Value == record["PersonID"]) ?
                    0 : (int)record["PersonID"];

                p.Name = (DBNull.Value == record["Name"]) ?
                    string.Empty : (string)record["Name"];

                p.Mobile = (DBNull.Value == record["Mobile"]) ?
                    string.Empty : (string)record["Mobile"];

                p.Email = (DBNull.Value == record["Email"]) ?
                    string.Empty : (string)record["Email"];

                return p;
            }
            catch
            {
                throw;

                // NOTE: 
                // consider handling exeption here instead of re-throwing
                // if graceful recovery can be accomplished
            }
        }
    }
}
