namespace FS.DataObject.User.Request
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using Utils.Enums;

    public class EditUserResquest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public EGender Gender { get; set; }
    }
}