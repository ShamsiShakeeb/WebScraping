using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebScrapingSite.ViewModel
{
    public class ScarpingFilterViewModel
    {
        public ScarpingFilterViewModel()
        {
            StartDate = DateTime.Today.AddDays(-30);
            EndDate = DateTime.Now;
        }

        [Display(Name = "Start Date")]
        public DateTime StartDate { set; get; }

        [Display(Name = "End Date")]
        public DateTime EndDate { set; get; }
    }
}
