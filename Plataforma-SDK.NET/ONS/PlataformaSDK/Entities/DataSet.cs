using System.Collections.Generic;
using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
    public class DataSet 
    {
        public IDomainContext Entities{get; set;}


        public override bool Equals(object obj)
        {
            var @DataSet = obj as DataSet;
            return @DataSet != null &&
                   Entities.Equals(DataSet.Entities);
        }

        public override int GetHashCode()
        {
            var hashCode = -629850613;
            hashCode = hashCode * -1521134295 + EqualityComparer<IDomainContext>.Default.GetHashCode(Entities);
            return hashCode;
        }
    }
}