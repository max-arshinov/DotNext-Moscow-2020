using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Infrastructure.SwaggerSchema.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.SwaggerSchema.Filters
{
    public class SchemaOperationFilter : IOperationFilter
    {
        public SchemaOperationFilter()
        {
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var listOfParamDescr = new List<ControllerParameterDescriptor>();
            var paramDescriptor = context.ApiDescription.ActionDescriptor.Parameters.ToList();

            foreach (var item in paramDescriptor)
                listOfParamDescr.Add(item as ControllerParameterDescriptor);

            AddAdditionalAttributesToSchemas(operation, listOfParamDescr);
        }
        private void AddAdditionalAttributesToSchemas(OpenApiOperation operation, List<ControllerParameterDescriptor> list)
        {
            List<PropertyInfo> listOfProperties;

            foreach (var item in list)
            {
                if (item.ParameterInfo.CustomAttributes.Count() == 0)
                    continue;

                if (item.ParameterInfo.CustomAttributes.Any(x => x.AttributeType.Name == "FromFormAttribute"))
                {
                    listOfProperties = item.ParameterType.GetProperties().ToList();
                    foreach (var prop in listOfProperties)
                    {
                        if (prop.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
                            operation.RequestBody.Content.FirstOrDefault(x => x.Key == "multipart/form-data")
                                .Value.Schema.Properties.FirstOrDefault(x => x.Key == prop.Name)
                                    .Value.Extensions.Add("title", new OpenApiString(displayAttribute.GetName()));

                        if (prop.GetCustomAttribute(typeof(HiddenInputAttribute)) is HiddenInputAttribute hiddenInputAttribute)
                            operation.RequestBody.Content.FirstOrDefault(x => x.Key == "multipart/form-data")
                                .Value.Schema.Properties.FirstOrDefault(x => x.Key == prop.Name)
                                   .Value.Extensions.Add("isHidden", new OpenApiBoolean(hiddenInputAttribute.DisplayValue));

                        if (prop.PropertyType.IsArray || prop.PropertyType.IsEnum && prop.PropertyType.GetCustomAttribute(typeof(FlagsAttribute)) != null)
                            operation.RequestBody.Content.FirstOrDefault(x => x.Key == "multipart/form-data")
                                .Value.Schema.Properties.FirstOrDefault(x => x.Key == prop.Name)
                                    .Value.Extensions.Add("isMultiSelect", new OpenApiBoolean(true));

                        if (prop.GetCustomAttribute(typeof(DataTypeAttribute)) is DataTypeAttribute dataTypeAttribute)
                            operation.RequestBody.Content.FirstOrDefault(x => x.Key == "multipart/form-data")
                                .Value.Schema.Properties.FirstOrDefault(x => x.Key == prop.Name)
                                    .Value.Extensions.Add("dataType", new OpenApiString(dataTypeAttribute.GetDataTypeName()));

                        if (prop.GetCustomAttribute(typeof(MediaDataAttribute)) is MediaDataAttribute mediaDataAttribute)
                        {
                            var valueExtensions = 
                                operation.RequestBody.Content.FirstOrDefault(x => x.Key == "multipart/form-data")
                                    .Value.Schema.Properties.FirstOrDefault(x => x.Key == prop.Name)
                                    .Value.Extensions;
                            
                            valueExtensions.Add("mediaPropertyName", new OpenApiString(mediaDataAttribute.MediaTypePropertyName));
                        }
                        
                        if (prop.GetCustomAttribute(typeof(MediaTypeAttribute)) is MediaTypeAttribute mediaTypeAttribute)
                        {
                            var valueExtensions = 
                                operation.RequestBody.Content.FirstOrDefault(x => x.Key == "multipart/form-data")
                                .Value.Schema.Properties.FirstOrDefault(x => x.Key == prop.Name)
                                .Value.Extensions;
                            
                            valueExtensions.Add("mediaTypes", new OpenApiString(mediaTypeAttribute.AcceptedMediaTypes));
                            valueExtensions.Add("multipleFilesLoading", new OpenApiBoolean(mediaTypeAttribute.MultipleFilesLoading));
                            valueExtensions.Add("maxFileSizeInMb", new OpenApiInteger(mediaTypeAttribute.MaxFileSizeInMb));
                            valueExtensions.Add("maxFilesCount", new OpenApiInteger(mediaTypeAttribute.MaxFilesCount));
                            valueExtensions.Add("dropZoneLabel", new OpenApiString(mediaTypeAttribute.DropZoneLabel));
                            if(mediaTypeAttribute.MediaDataPropertyName!=null)
                                valueExtensions.Add("mediaDataPropertyName", new OpenApiString(mediaTypeAttribute.MediaDataPropertyName));
                        }
                    }
                }
            }
        }

    }
}