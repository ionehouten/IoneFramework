using Ione.Framework.Rest;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ione.Framework.Test
{
    public class ModelTempDataKta : Model<TempDataKta, ITempDataKta, OTempDataKtas, OTempDataKta>
    {
        public override string RestClient => "https://sahabatdemokrat.id";

        public override string EndpointGet => "/bpokk/public/get/members/{Page}";

        public override string EndpointGetRow => throw new System.NotImplementedException();

        public override string EndpointPost => throw new System.NotImplementedException();

        public override string EndpointPut => throw new System.NotImplementedException();

        public override string EndpointDelete => throw new System.NotImplementedException();

        public override string EndpointDownload => throw new System.NotImplementedException();

        public ModelTempDataKta()
        {
            this.RequestHeader = new System.Collections.Hashtable
            {
                { "key", "BPOKK_1245" }
            };
        }
    }

    public partial class TempDataKta 
    {

    
        [JsonPropertyName("membership_id")]
        public virtual string Id { get; set; }

        [JsonPropertyName( "member_name")]
        public virtual string MemberName { get; set; }

        [JsonPropertyName( "nik")]
        public virtual string Nik { get; set; }

        [JsonPropertyName( "cellular_number")]
        public virtual string CellularNumber { get; set; }

        [JsonPropertyName( "gender")]
        public virtual string Gender { get; set; }

        [JsonPropertyName( "birth_place")]
        public virtual string BirthPlace { get; set; }

        [JsonPropertyName( "birth_date")]
        public virtual string BirthDate { get; set; }

        [JsonPropertyName( "profession")]
        public virtual string Profession { get; set; }

        [JsonPropertyName( "religion")]
        public virtual string Religion { get; set; }

        [JsonPropertyName( "education")]
        public virtual string Education { get; set; }

        [JsonPropertyName( "address")]
        public virtual string Address { get; set; }

        [JsonPropertyName( "rt")]
        public virtual string Rt { get; set; }

        [JsonPropertyName( "rw")]
        public virtual string Rw { get; set; }

        [JsonPropertyName( "kodepos")]
        public virtual string KodePos { get; set; }

        [JsonPropertyName( "province")]
        public virtual string Province { get; set; }

        [JsonPropertyName( "kabupaten")]
        public virtual string Kabupaten { get; set; }

        [JsonPropertyName( "kecamatan")]
        public virtual string Kecamatan { get; set; }

        [JsonPropertyName( "kelurahan")]
        public virtual string Kelurahan { get; set; }

        [JsonPropertyName( "photo")]
        public virtual string Photo { get; set; }

        [JsonPropertyName( "ktp")]
        public virtual string Ktp { get; set; }



    }

    public class OTempDataKtas : Response<List<TempDataKta>, OTempDataKtas>
    {
        [JsonPropertyName( "total_page")]
        public int? TotalPage { get; set; }

        [JsonPropertyName( "total_data")]
        public int? TotalData { get; set; }
    }

    public class OTempDataKta : Response<TempDataKta, OTempDataKta> { }

    public class ITempDataKta : Request<TempDataKta>
    {
    }
}
