using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetList
{
    public class Request : IRequest<IList<DOOrder>>
    {        
        public Request(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        /// <summary>
        /// Get List of DOOrder with paging
        /// Page : the page number of DOOrder data you want to display
        /// PageSize : the number of rows of DOOrder data on each page that you want to display
        /// </summary>

        //[Range(0, Int32.MaxValue, ErrorMessage = "Enter page greater than 0 ")]
        [BindProperty(Name = "page")]
        public int Page { get; }
        //[Range(0, Int32.MaxValue, ErrorMessage = "Enter pageSize greater than 0 ")]
        [BindProperty(Name = "PageSize")]
        public int PageSize { get; }


    }
}
