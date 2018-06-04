using System.Collections.Generic;

namespace ONS.PlataformaSDK.Entities
{
    public class Reproduction
    {
        public string From;
        public string To;

        public override bool Equals(object obj)
        {
            var @reproduction = obj as Reproduction;
            return @reproduction != null &&
                   From == @reproduction.From &&
                   To == @reproduction.To;
        }

        public override int GetHashCode()
        {
            var hashCode = -629850613;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(From);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(To);
            return hashCode;
        }
    }
}