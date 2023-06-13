using Ione.Framework.Core.Authenticator;
using Ione.Framework.Rest;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ione.Framework.Test
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //JwtAuthenticator auth = new JwtAuthenticator()
            //{
            //    AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjgxYmZiNWUzLTc5ZDEtNGE4OS1iZTlhLWNlMzk1ZjI5ZGQ2YyIsIkVtYWlsIjoiaW9uZS50aGVhMjdAZ21haWwuY29tIiwiVXNlcm5hbWUiOiJhZG1pbiIsIkZ1bGxuYW1lIjoiSXdhbiBTZXRpYXdhbiIsIlRlbHAiOiIiLCJVcmxQaG90byI6IiIsIklzTXVsdGlwbGUiOiJUcnVlIiwiQXBwSWQiOiJBUFAtRFRBIiwiUm9sZUlkIjoiOGU2ZDM4ODEtOGNlOC00NWNjLThmMTQtYWM3ZTAwYzg3NjliIiwiUmVmSWQiOiIiLCJuYmYiOjE2MDY3MTIzOTQsImV4cCI6MTYwNjc0MTE5NCwiaXNzIjoiLSIsImF1ZCI6Ii0ifQ.gj-fa1ZxV8EtGg0hDppc6Bv0WKmUU_LZt2tSx7J00ek"
            //};
            //InputModel model = new InputModel(auth)
            //{

            //};
            //OMenus output = await model.GetMenuAsync(new IMenu() { });



            ModelTempDataKta model = new ModelTempDataKta();
            try
            {
                OTempDataKtas list = new OTempDataKtas();
                ITempDataKta input = new ITempDataKta()
                {
                    Page = 1,
                    PageSize = 10,
                };
                var data = await model.GetAsync(input);
            }
            catch (Exception)
            {
                throw ;
            }



        }
    }

    public class InputModel : Model<Menu, IMenu, OMenus, OMenu>
    {
        public override string RestClient => "https://localhost:9001/";

        public override string EndpointGet => "apiauth/v1/Menu";

        public override string EndpointPost => "SvcAuthl";

        public override string EndpointPut => throw new NotImplementedException();

        public override string EndpointDelete => throw new NotImplementedException();

        public override string EndpointGetRow => throw new NotImplementedException();

        public override string EndpointDownload => throw new NotImplementedException();

        public InputModel(JwtAuthenticator auth)
        {
            this.Authenticator = auth;
        }
        public async Task<OMenus> GetMenuAsync(IMenu input)
        {
            return await GetAsync(EndpointGet, input) as OMenus;
        }
    }


    public partial class Menu 
    {

        public virtual Guid? Id { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual string AppId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual string Controller { get; set; }
        public virtual string Action { get; set; }
        public virtual string Icon { get; set; }
        public virtual short? Sort { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual string Type { get; set; }
        public virtual string SubType { get; set; }
        public virtual string Position { get; set; }
        public virtual bool? IsAjax { get; set; }
        [JsonIgnore] public virtual string CreatedBy { get; set; }
        [JsonIgnore] public virtual DateTime? CreatedAt { get; set; }
        [JsonIgnore] public virtual string UpdatedBy { get; set; }
        [JsonIgnore] public virtual DateTime? UpdatedAt { get; set; }
        public virtual List<Menu> Children { get; set; }


      
    }

    public class OMenus : Response<List<Menu>, OMenus> { }

    public class OMenu : Response<Menu, OMenu> { }

    public class IMenu : Request<Menu> { }

    public partial class SortMenu 
    {
        public virtual Guid? Id { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual short? Sort { get; set; }
        public virtual SortMenuList Children { get; set; }
    }
    public class SortMenuList : List<SortMenu> { };
    public class OSortMenus : Response<List<SortMenu>, OSortMenus> { }
    public class OSortMenu : Response<SortMenu, OSortMenu> { }
    public class ISortMenu : Request<SortMenu> { }

}
