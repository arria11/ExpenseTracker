using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class TagService
    {
        public List<string> GetTags()
        {
            return new List<string>
            {
                "Yearly",
                "Monthly",
                "Food",
                "Drinks",
                "Clothes",
                "Gadgets",
                "Miscellaneous",
                "Fuel",
                "Rent",
                "EMI",
                "Party",
                "Salary",
                "Shopping",
                "Other"
            };
        }
    }
}
