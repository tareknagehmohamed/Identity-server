using System.Security.Claims;
using System.Text.Json;

namespace IdentityServerAccountJwt.Client.Services
{
    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims=new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes=ParseBase64WithoutBadding(payload);
            var KeyValuePairs=JsonSerializer.Deserialize<Dictionary<string,object>>(jsonBytes);
            ExtractRolesFromJwt(claims, KeyValuePairs); 
            claims.AddRange(KeyValuePairs.Select(kvp=>new Claim(kvp.Key,kvp.Value.ToString())));
            return claims;
        }
        private static byte[]ParseBase64WithoutBadding(string base64)
        {
            switch(base64.Length%4) {
                case 2:base64 += "==";break;
                case 3:base64 += "=";break;
            }
           return Convert.FromBase64String(base64);
        }
        public static void ExtractRolesFromJwt(List<Claim>claims,Dictionary<string,object>keyValuePairs) {

            keyValuePairs.TryGetValue(ClaimTypes.Role,out object roles);
            if (roles!=null)
            {
                var parsedroles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');
                if (parsedroles.Length>1)
                {
                    foreach (var parsedrole in parsedroles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role,parsedrole.Trim('"')));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedroles[0]));

                }
                keyValuePairs.Remove(ClaimTypes.Role);
            }
        }
    }
}
