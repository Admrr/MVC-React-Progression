using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Practice4.Paginator
{
    public class Page<T>
    {
        public int Index { get; set; }
        public T[] Items { get; set; }
        public int TotalPages { get; set; }
    }

    public static class MyExtensions
    {
        public static Page<T> Paginator<T>(this Microsoft.EntityFrameworkCore.DbSet<T> List, int index_page, int page_size, Func<T, object> order_by_selector) where T : class
        {
            var result = List.OrderBy(order_by_selector).Skip(index_page * page_size).Take(page_size).ToArray();
            // Safeguard for the query in case it fails for whatever reason.
            if(result == null || result.Length == 0) {
                return null;
            }
            // We calculate the total amount of items of the input List here.
            var total_items = List.Count();
            // We calculate the total amount of pages here with a safeguard.
            var total_pages = total_items / page_size;
            // Safeguard for the calculation.
            if(total_pages < page_size) {
                total_pages = 1;
            }
            return new Page<T>
            {
                Index = index_page,
                Items = result,
                TotalPages = total_pages
            };
        }

    }
}