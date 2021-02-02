using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Edm;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.OData.UriParser;
using Microsoft.AspNetCore.Cors.Infrastructure;
using EJ2DocumentEditorServer.Controllers;
using Syncfusion.EJ2.SpellChecker;

namespace EJ2DocumentEditorServer
{
    public class Startup
    {
        internal static List<SpellCheckDictionary> spellDictCollection;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            string path = Configuration["SPELLCHECK_DICTIONARY_PATH"];
            if(path !=string.Empty)
            {
            string dictionaryPath = path+"/en_US.dic";
            string affixPath = path + "/en_US.aff";
            string customDict = path + "/customDict.dic";
            List<DictionaryData> items = new List<DictionaryData>() {
               new DictionaryData(1046,dictionaryPath,affixPath,customDict),
            };
            spellDictCollection = new List<SpellCheckDictionary>();
            foreach (var item in items)
            {
                spellDictCollection.Add(new SpellCheckDictionary(new DictionaryData(1046, dictionaryPath, affixPath, customDict)));
            }
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();
            services.AddMvc().AddJsonOptions(x => {
                x.SerializerSettings.ContractResolver = null;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
           
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            string license_key = Configuration["SYNCFUSION_LICENSE_KEY"];
            if (license_key!=null && license_key!=string.Empty)
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(license_key);
            app.UseDeveloperExceptionPage();
            app.UseCors("AllowAllOrigins");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{api}/{controller}/{action}/{id?}");
            });
        }

    }
}
