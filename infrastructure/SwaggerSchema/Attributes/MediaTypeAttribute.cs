using System;

namespace Infrastructure.SwaggerSchema.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MediaTypeAttribute: Attribute
    {
        public MediaTypeAttribute(string acceptedMediaTypes = ".jpg,.png,.jpeg,.heic,.heif", 
            bool multipleFilesLoading = false, 
            int maxFilesCount = 1, 
            string dropZoneLabel = "загрузка файлов", 
            int maxFileSizeInMb = 2, 
            string mediaDataPropertyName = null)
        {
            AcceptedMediaTypes = acceptedMediaTypes;
            MultipleFilesLoading = multipleFilesLoading;
            MaxFilesCount = maxFilesCount;
            DropZoneLabel = dropZoneLabel;
            MaxFileSizeInMb = maxFileSizeInMb;
            MediaDataPropertyName = mediaDataPropertyName;
        }

        public string AcceptedMediaTypes { get; }
        public bool MultipleFilesLoading { get; }
        public int MaxFilesCount { get; }
        public string DropZoneLabel { get; }
        public int MaxFileSizeInMb { get; }
        public string MediaDataPropertyName { get; }
    }
}