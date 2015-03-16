using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace W_GJS.Models
{
    public class SearchNewsResultModel
    {
        public List<O_NEWS> ResultList { get; set; }
        public int NumberOfItemFound {  get; set; }
        public bool IsSearching { get; set; }
        public string keyword { get; set; }

    }
}