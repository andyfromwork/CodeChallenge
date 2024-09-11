using System.Diagnostics;
using CodeChallenge.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChallenge.Config
{
    public static class WebApplicationBuilderExt
    {
        // I debated having a switch/case here or possibly passing in a list of strings for names of the DB's if this were to grow over time.
        // For this use case I think having them as static strings and doing it 100% of the time fits.
        private static readonly string DB_Emp = "EmployeeDB";
        private static readonly string DB_Comp = "CompensationDB";

        /// <summary>
        /// Sets the In Memory DB Details.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void UseDBs(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseInMemoryDatabase(DB_Emp);
            });

            builder.Services.AddDbContext<CompensationContext>(options =>
            {
                options.UseInMemoryDatabase(DB_Comp);
            });
        }
    }
}
