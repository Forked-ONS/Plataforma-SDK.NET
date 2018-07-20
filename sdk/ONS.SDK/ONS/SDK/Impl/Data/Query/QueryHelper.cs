using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace ONS.SDK.Impl.Data.Query
{
    public static class QueryHelper
    {
        public static IList<string> GetParametersName(string query)
        {
            return Regex.Matches(query, @"[$|:]\w+").Select(m => m.Value.Substring(1)).ToList();
        }

        public static IDictionary<string, object> MakeParameters(
            string query, 
            JObject objParameter, 
            out bool notContainAnyParameter) 
        {
            return MakeParameters(
                GetParametersName(query), 
                objParameter, 
                out notContainAnyParameter
            );
        }

        public static IDictionary<string, object> MakeParameters(
            IList<string> parametersName, 
            object objParameter) 
        {
            bool notContainAnyParameter;
            return MakeParameters(
                parametersName, 
                JObject.FromObject(objParameter), 
                out notContainAnyParameter
            );
        }

        public static IDictionary<string, object> MakeParameters(
            IList<string> parametersName, 
            JObject objParameter, 
            out bool notContainAnyParameter) 
        {
            var retorno = new Dictionary<string, object>();
            notContainAnyParameter = true;
            
            if (parametersName != null && parametersName.Any()) {
                foreach(var paramName in parametersName) {
                    var token = objParameter.SelectToken(paramName);
                    
                    if (token != null) {
                        retorno[paramName] = token.ToString();
                    } else {
                        notContainAnyParameter = false;
                    }
                }
            }

            return retorno;
        }
    }
}