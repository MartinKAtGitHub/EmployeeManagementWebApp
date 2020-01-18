using Portfolio_Website_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.ViewModels
{
    /// <summary>
    /// ViewModels job is just to take all the information that a view needs an put it into 1 class.
    /// becasue somethims 1 Model might not be enough for a view and this class will provide as with a place to pass as much information as we want  to the view
    /// </summary>
    public class HomeDetailsViewModel 
    {
        public Employee Employee{ get; set; }
        public string PageTitle { get; set; }

        public IEnumerable<Comment> comments { get; set; }
    }
}
