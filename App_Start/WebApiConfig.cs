using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NodeDisplay
{
    public static class WebApiConfig
    {

        public static string ControllerOnly = "ApiControllerOnly";
        public static string ControllerAndId = "ApiControllerAndIntegerId";
        public static string ControllerAction = "ApiControllerAction";
        public static string ControllerActionAndId = "ApiControllerActionAndIntegerId";
        public static void Register(HttpConfiguration config)
        {
            // This controller-per-type route is ideal for GetAll calls.            
            // It finds the method on the controller using WebAPI conventions          
            // The template has no parameters.           
            //           
            // ex: api/sessionbriefs            
            // ex: api/sessions            
            // ex: api/persons
            config.Routes.MapHttpRoute(
                name: ControllerOnly,
                routeTemplate: "api/{Controller}"
                );

            // This controller-per-type route lets us fetch a single resource by numeric id            // It finds the appropriate method GetById method            // on the controller using WebAPI conventions            // The {id} is not optional, must be an integer, and             // must match a method with a parameter named "id" (case insensitive)            //            //  ex: api/sessions/1            //  ex: api/persons/1
            config.Routes.MapHttpRoute(
                name: ControllerAndId,
                routeTemplate: "api/{controller}/{id}",
                defaults: null,
                constraints: new { id = @"^\d+$" }
            );
            config.Routes.MapHttpRoute(
                name: ControllerAction,
                routeTemplate: "api/{controller}/{action}"
                );
            // This RPC style route is great for lookups and custom calls           
            // It matches the {action}/{id} to a method on the controller             
            //            
            // ex: api/lookups/all            
            // ex: api/lookups/room/3           
            config.Routes.MapHttpRoute(            
                name: ControllerActionAndId,            
                routeTemplate: "api/{controller}/{action}/{id}"
                );
        }
    }
    }

