using System;
using DataProvider;
using System.Collections.ObjectModel;
using DataModels;

namespace JISCompetition
{
    public partial class LoginPage : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            QueryPerson query = new QueryPerson();
            Collection<Person> test = query.GetPersons();
        }
    }
}