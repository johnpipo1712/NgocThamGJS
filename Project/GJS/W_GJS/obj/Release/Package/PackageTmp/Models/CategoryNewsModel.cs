using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace W_GJS.Models
{
    public class CategoryNewsModel : PagingModel
    {
        public List<O_NEWS> NewsList { get; set; }
    }
}